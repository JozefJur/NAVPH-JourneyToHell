public interface ActiveItemTemplate : ItemTemplate
{

    Type getType();



    public enum Type
    {
        INSTANT,
        DURATION
    }

}
