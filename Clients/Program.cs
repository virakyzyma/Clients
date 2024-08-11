namespace Clients
{
    internal class Program
    {
        static void Main()
        {
            Queue<Customer> customers = new Queue<Customer>();
            for (int i = 0; i <= 20; i++)
            {
                customers.Enqueue(new Customer(i));
            }
            List<CashRegister> cashRegisters = new List<CashRegister>();
            for (int i = 0; i <= 3; i++)
            {
                cashRegisters.Add(new CashRegister(customers, i));
            }
            foreach (CashRegister cashRegister in cashRegisters)
            {
                cashRegister.Start();
            }
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public Customer(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
    public class CashRegister
    {
        private Queue<Customer> _customers;
        private Thread _thread;
        private int _registerNumber;
        private static readonly Random _random = new Random();

        public CashRegister(Queue<Customer> customers, int registerNumber)
        {
            _customers = customers;
            _thread = new Thread(ProcessCustomers);
            _registerNumber = registerNumber;
        }
        public void Start() => _thread.Start();
        private void ProcessCustomers(object? obj)
        {
            while (true)
            {
                Customer customer;
                lock (_customers)
                {
                    if (_customers.Count == 0)
                    {
                        break;
                    }
                    customer = _customers.Dequeue();
                }
                Console.WriteLine($"Casa {_registerNumber} start working for {customer.Id}");
                int time = _random.Next(1, 4) * 1000;
                Thread.Sleep(time);
                Console.WriteLine($"Casa {_registerNumber} fineshed working for {customer.Id}, in {time / 1000} seconds");
            }
        }
    }
}