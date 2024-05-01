using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    abstract class Person : IComparable<Person>
    {
        private string _fullName;
        private string _id;
        private int _points;
        private double _totalInvoiced;
        private bool active;

        protected Person(string id, string fullName, int points)
        {
            if (id.Length != 8 || id == null) throw new Exception("INVALID ID");
            if (fullName == null) throw new ArgumentNullException("INVALID NAME");
            if (points < 0) throw new ArgumentOutOfRangeException("INVALID POINTS");

            this._id = id;
            this._fullName = fullName;
            this._points = points;
            this._totalInvoiced = 0;
            this.active = false;
        }

        protected Person(string id, string fullName) : this(id, fullName, 0) { }

        public string FullName { get { return _fullName; } }
        
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public abstract double GetRating();

        public void AddInvoicedAmount(double amount)
        {
            if (amount == default(double)) throw new ArgumentNullException("INVALID AMOUNT");
            if (amount <= 0) throw new ArgumentOutOfRangeException("INVALID AMOUNT");

            _totalInvoiced += amount;
        }

        public abstract void AddPoints(int points);

        public int CompareTo(Person? other)
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            string disponible;

            if (active) disponible = "DISPONIBLE -> N";
            else disponible = "DISPONIBLE -> S";

            return disponible;
        }
    }
}
