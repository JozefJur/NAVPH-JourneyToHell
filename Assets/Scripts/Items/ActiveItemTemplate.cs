public interface ActiveItemTemplate : ItemTemplate
{

    Type getType();

    bool isActive();

    public enum Type
    {
        INSTANT,
        DURATION
    }

}
