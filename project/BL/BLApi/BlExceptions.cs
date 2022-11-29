namespace BO;

public class BlEntityNotFoundException : Exception
{
    public BlEntityNotFoundException(Exception inner) : base("Entity not found", inner) { }
    public override string Message => "Entity not found";
}

public class BlEntityAlreadyExistException : Exception
{
    public BlEntityAlreadyExistException(Exception inner) : base("entity already exists", inner) { }
    public override string Message => "entity already exists";
}

public class BlNoEntitiesFound : Exception
{
    public readonly string msg;
    public BlNoEntitiesFound(string m) { msg = m; }
    public override string Message => msg;

}
public class BlOutOfStockException : Exception
{

    public BlOutOfStockException(Exception inner) : base("product is out of stock", inner) { }
    public override string Message => ("product is out of stock");
}


public class BlInvalidIntegerException : Exception
{
    public BlInvalidIntegerException() : base("invalid input: not integer") { }
    public override string Message => ("invalid input: not integer");
}

public class CustomerDetailsAreInValid : Exception
{
    public CustomerDetailsAreInValid(Exception inner) : base("the details are invalid", inner) { }
    public override string Message => ("the details are invalid");
}

public class BlExceptionFailedToRead : Exception
{
    public BlExceptionFailedToRead(Exception inner) : base("failed to read", inner) { }
    public override string Message => ("failed to read");
}
public class BlInvalidIdToken : Exception
{
    public readonly string msg;
    public BlInvalidIdToken(string m) { msg = m; }
    public override string Message => msg;
}
public class BlInvalidNameToken : Exception
{
    public readonly string msg;
    public BlInvalidNameToken(string m) { msg = m; }
    public override string Message => msg;
}

public class BlInvalidPriceToken : Exception
{
    public readonly string msg;
    public BlInvalidPriceToken(string m) { msg = m; }
    public override string Message => msg;
}

public class BlProductExistsInAnOrder : Exception
{
    public readonly string msg;
    public BlProductExistsInAnOrder(string m) { msg = m; }
    public override string Message => msg;
}

public class blInvalidAmountToken : Exception
{
    public readonly string msg;
    public blInvalidAmountToken(string m) { msg = m; }
    public override string Message => msg;
}

