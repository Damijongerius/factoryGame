

using System;

public interface ITileBehavior
{
    public void Run();

    public void Execute(Object obj);
    
    public void Initialize(ITile tile);
}
