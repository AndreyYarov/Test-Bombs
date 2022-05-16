using System.Collections;

public abstract class EntityPipeline<P, T>
    where P : EntityPipeline<P, T>, new()
    where T : Template
{
    public static P Create(T template)
    {
        var instance = new P();
        instance.template = template;
        return instance;
    }

    private T template;
    protected T Template => template;

    public abstract IEnumerator Run();
}
