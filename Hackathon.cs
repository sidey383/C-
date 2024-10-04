using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {

    public class Hackathon {

        private readonly System.Random random = new System.Random();

        public Hackathon() {
            Console.WriteLine("Hackathon create!");
        }

        public IEnumerable<Wishlist> CreateWishlist(IEnumerable<Employee> employee, IEnumerable<Employee> toDesire) {
            int[] targets = toDesire.ToList().ConvertAll(p => p.Id).ToArray();
            Wishlist[] wishes = new Wishlist[employee.Count()];
            IEnumerator<Employee> ie = employee.GetEnumerator();
            for (int i = 0; i < wishes.Length; i++) {
                ie.MoveNext();
                int[] order = new int[targets.Length];
                Array.Copy(targets, order, targets.Length);
                random.Shuffle(order);
                wishes[i] = new Wishlist(ie.Current.Id, order);
            }
            return wishes;
        }

    }

}