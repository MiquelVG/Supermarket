using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Cashier : Person
    {
        private DateTime _joiningDate;

        public Cashier(string id, string fullName, DateTime hired) : base (id, fullName) 
        {
            this._joiningDate = hired;
        }

        public int YearsOfService 
        {
            get 
            {

                TimeSpan difference = DateTime.Now - _joiningDate;
                int years = (int)difference.TotalDays / 365;

                return years;
            }
        }

        public override double GetRating { get { return (this.YearsOfService * 365.25) * ((this._totalInvoiced * 10) / 100); } }

        public override void AddPoints(int pointsToAdd)
        {
            if (pointsToAdd <= 0) throw new IndexOutOfRangeException("POINTS CAN'T BE NEGATIVE OR ZERO");
            if (pointsToAdd is default(int)) throw new ArgumentNullException("POINTS CAN'T BE NULL");

            _points += (this.YearsOfService + 1 ) * pointsToAdd;  
        }

        public override string ToString()
        {
            return $"DNI/NIE -> {_id}  NOM -> {_fullName}       RATING -> {this.GetRating}  ANTIGUITAT -> {this.YearsOfService} VENDES -> {_totalInvoiced} \u20AC     PUNTS -> {_points}  {base.ToString()}";
        }
    }
}
