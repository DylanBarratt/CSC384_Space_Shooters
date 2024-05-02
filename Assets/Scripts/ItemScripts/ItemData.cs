[System.Serializable]
public class ItemData {
    public int id, cost, value;

    public ItemData(int id, int cost, int value) {
        this.id = id;
        this.cost = cost;
        this.value = value;
    }
}