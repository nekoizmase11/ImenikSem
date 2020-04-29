using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.BiznisModeli
{
    public class PaginacijaBiznisModel<T>
    {
		public int TrenutnaStrana { get; private set; }
		public int UkupnoStrana { get; private set; }
		public int BrojKontakataStranice { get; private set; }
		public int UkupnoKontakata { get; private set; }

		public bool ImaPrethodni => TrenutnaStrana > 1;
		public bool ImaSledeci => TrenutnaStrana < UkupnoStrana;
		public List<T> Kontakti { get; set; }
		public List<int> RaspoloziveStrane { get; set; }

		public PaginacijaBiznisModel(List<T> items, int count, int pageNumber, int pageSize, int ukupnoStrana,List<int> raspoloziveStrane)
		{
			UkupnoKontakata = count;
			BrojKontakataStranice = pageSize;
			TrenutnaStrana = pageNumber;
			UkupnoStrana = ukupnoStrana;
			RaspoloziveStrane = raspoloziveStrane;
			//UkupnoStrana = (int)Math.Ceiling(count / (double)pageSize);


			Kontakti = items;
			//AddRange(items);
		}

	}
}
