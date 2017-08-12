namespace SerializationDemo
{
    [Service("stringService")]
    public class SomeService
    {
        [Operation("concat")]
        public C Concatenate([OperationArg("itemA")]A a, B itemB)
        {
            return new C
            {
                Value = a.Value + itemB.Value
            };
        }
    }

}
