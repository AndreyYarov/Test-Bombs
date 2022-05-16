using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UniRx;

using Object = UnityEngine.Object;

public abstract class Presenter<V, P>
    where V : View<V, P>
    where P : Presenter<V, P>, new()
{
    private class TypeInfo
    {
        public readonly MethodInfo Start, Update, OnDestroy;
        public TypeInfo(Type type)
        {
            Start = type.GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
            Update = type.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            OnDestroy = type.GetMethod("OnDestroy", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static Dictionary<Type, TypeInfo> types = new Dictionary<Type, TypeInfo>();
        public static TypeInfo Get<T>()
        {
            Type type = typeof(T);
            if (!types.TryGetValue(type, out var typeInfo))
            {
                typeInfo = new TypeInfo(type);
                types.Add(type, typeInfo);
            }
            return typeInfo;
        }
    }

    private V view;
    protected V View
    {
        get
        {
            if (view == null)
            {
                view = Object.FindObjectOfType<V>();
                view.Presenter = (P)this;
            }
            return view;
        }
    }

    protected static P FindPresenter()
    {
        return Object.FindObjectOfType<V>().Presenter;
    }

    public void SetView(V view)
    {
        this.view = view;
    }

    private CompositeDisposable coroutines = new CompositeDisposable();
    protected IDisposable StartCoroutine(IEnumerator routine)
    {
        var c = routine.ToObservable().Subscribe();
        c.AddTo(coroutines);
        return c;
    }

    protected void StopCoroutine(IDisposable routine)
    {
        if (coroutines.Contains(routine))
        {
            coroutines.Remove(routine);
            routine.Dispose();
        }
    }

    protected void StopAllCoroutines()
    {
        coroutines.Dispose();
        coroutines = new CompositeDisposable();
    }

    private IDisposable updateLoop, startLoop;
    private TypeInfo typeInfo;
    protected Presenter()
    {
        typeInfo = TypeInfo.Get<P>();

        if (typeInfo.Start != null)
        {
            if (typeInfo.Start.ReturnType == typeof(void))
                typeInfo.Start.Invoke(this, null);
            else if (typeInfo.Start.ReturnType == typeof(IEnumerator))
            {
                var start = (IEnumerator)typeInfo.Start.Invoke(this, null);
                startLoop = start.ToObservable().Subscribe();
            }
        }

        if (typeInfo.Update != null && typeInfo.Update.ReturnType == typeof(void))
            updateLoop = Observable.EveryUpdate().Subscribe(_ => typeInfo.Update.Invoke(this, null));
    }

    private bool destroyed = false;
    public void Destroy()
    {
        if (destroyed)
            return;
        destroyed = true;
        if (typeInfo.OnDestroy != null)
            typeInfo.OnDestroy.Invoke(this, null);
        if (startLoop != null)
            startLoop.Dispose();
        if (updateLoop != null)
            updateLoop.Dispose();
        StopAllCoroutines();
        if (view)
            Object.Destroy(view.gameObject);
    }
}

