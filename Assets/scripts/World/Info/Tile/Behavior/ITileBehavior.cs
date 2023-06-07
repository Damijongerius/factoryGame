

using System;

public interface ITileBehavior
{
    public void Run();

    public void Execute(ITile tile, object obj);
    
    public void Initialize(ITile tile);
}
