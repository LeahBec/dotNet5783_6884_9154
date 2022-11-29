namespace BO;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Exception inner) : base("Entity not found", inner) { }
    public override string Message => "Entity not found";
}

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(Exception inner) : base("entity already exists", inner) { }
    public override string Message => "entity already exists";
}

public class NoEntitiesFound : Exception
{
    public NoEntitiesFound(Exception inner) : base("no entities", inner) { }
    public override string Message => ("no entities");

}
public class OutOfStockException : Exception
{

    public OutOfStockException(Exception inner) : base("product is out of stock", inner) { }
    public override string Message => ("product is out of stock");
}


public class InvalidIntegerException : Exception
{
    public InvalidIntegerException() : base("invalid input: not integer") { }
    public override string Message => ("invalid input: not integer");
}

public class CustomerDetailsAreInValid : Exception
{
    public CustomerDetailsAreInValid(Exception inner) : base("the details are invalid", inner) { }
    public override string Message => ("the details are invalid");
}

public class BlExceptionFailedToRead : Exception
{
    public readonly string  msg;
    public BlExceptionFailedToRead(string m) { msg = m; }
    public override string Message => msg;    
}



