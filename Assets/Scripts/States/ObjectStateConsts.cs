public static class ObjectStateConsts
{
    public static class MoveStates
    {
        private static byte _currentId = 0;
        public static ObjectState Idle = new ObjectState(++_currentId);
        public static ObjectState Walk = new ObjectState(++_currentId);
        public static ObjectState Run = new ObjectState(++_currentId);
        public static ObjectState Slide = new ObjectState(++_currentId);
    }

    public static class HealthStates
    {
        private static byte _currentId = 0;
        public static ObjectState Healthy = new ObjectState(++_currentId);
        public static ObjectState Hit = new ObjectState(++_currentId);
        public static ObjectState Dead = new ObjectState(++_currentId);
    }

    public static class WeaponAttackState
    {
        private static byte _currentId = 0;
    }
}
