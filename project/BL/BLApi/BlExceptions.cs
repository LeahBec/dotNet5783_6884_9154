namespace BO;

public class BlEntityNotFoundException : Exception
{
    public BlEntityNotFoundException(DalApi.ExceptionObjectNotFound? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
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

    public BlOutOfStockException() : base("product is out of stock") { }
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
    public BlExceptionFailedToRead(DalApi.ExceptionFailedToRead? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}public class BlExceptionNoMatchingItems : Exception
{
    public BlExceptionNoMatchingItems(DalApi.ExceptionNoMatchingItems? inner = null) : base("no matching items", inner) { }
    public override string Message =>
                    "no matching items";
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
public class BlOrderAlreadyDelivered : Exception
{
    public readonly string msg;
    public BlOrderAlreadyDelivered(string m) { msg = m; }
    public override string Message => msg;
}
public class BlDefaultException : Exception
{
    public readonly string msg;
    public BlDefaultException(string m) { msg = m; }
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

