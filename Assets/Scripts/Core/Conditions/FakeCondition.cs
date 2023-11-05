namespace Core.Conditions
{
    public class FakeCondition : ICondition
    {

        private readonly bool alwaysIs;

        public FakeCondition(bool alwaysIs)
        {
            this.alwaysIs = alwaysIs;
        }

        public bool IsMet()
        {
            return alwaysIs;
        }
    }
}