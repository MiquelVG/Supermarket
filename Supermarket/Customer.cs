using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class Customer : Person
    {
        private int? _fidelity_card;

        public Customer (string id, string fullName, int? fidelityCard) : base (id, fullName) 
        { 
            _fidelity_card = fidelityCard;
        }

        #region PROPERTIES
        public int? FidelityCard { get { return _fidelity_card; } }
        public override double GetRating => Math.Round((_totalInvoiced * 2) / 100, 2);
        #endregion

        #region OVERRIDE
        public override void AddPoints(int pointsToAdd)
        {
            if (pointsToAdd < 0) throw new ArgumentOutOfRangeException("POINTS CAN'T BE NEGATIVE");

            if (_fidelity_card is not null) _points += pointsToAdd;
        }

        public override string ToString()
        {
            return $"DNI/NIE->{_id} NOM->{_fullName}        RATING->{GetRating}     VENDES ->{_totalInvoiced}   \u20AC PUNTS->{_points}     {base.ToString()}";
        }
        #endregion

    }
}
