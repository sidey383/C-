namespace C_;

public record Wishlist(int EmployeeId, int[] DesiredEmployees) {

    public static Wishlist CreateRandom<T>(int owner, IEnumerable<T> availiable, System.Random random) where T : Employee {
        int[] copy = new int[availiable.Count()];
        IEnumerator<T> ie = availiable.GetEnumerator();
        for (int i = 0; ie.MoveNext(); i++) {
            copy[i] = ie.Current.GetId();
        }
        random.Shuffle(copy);
        return new Wishlist(owner, copy);
    }

    public int GetPose(int id) {
        for (int i = 0; i < DesiredEmployees.Length; i++) {
            if (DesiredEmployees[i] == id)
                return i;
        }
        return int.MaxValue;
    }

}