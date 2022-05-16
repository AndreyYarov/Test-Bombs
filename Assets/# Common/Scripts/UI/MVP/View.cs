using UnityEngine;

public abstract class View<V, P> : MonoBehaviour
    where V : View<V, P>
    where P : Presenter<V, P>, new()
{
    private P presenter;
    public P Presenter
    {
        get
        {
            if (presenter == null)
            {
                presenter = new P();
                presenter.SetView((V)this);
            }
            return presenter;
        }
        set
        {
            presenter = value;
        }
    }

    public virtual bool Visible
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }

    protected virtual void OnDestroy()
    {
        if (presenter != null)
            presenter.Destroy();
    }
}

