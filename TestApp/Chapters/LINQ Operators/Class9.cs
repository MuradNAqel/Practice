namespace TestApp.Chapters.Chapter_9
{
    public class Class9
    {
        string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
        //Filtering 
        //
        /* Where
         * 
                from n in names
                where n.Length > 3
                let u = n.ToUpper()
                where u.EndsWith ("Y")
                select u;
                // HARRY
                // MARY
        */
        /* Take  ROW_NUMBER function in SQL
                 The next query returns books 21 to 40:
                IQueryable<Book> query = dbContext.Books
                .Where (b => b.Title.Contains ("mercury"))
                .OrderBy (b => b.Title)
                .Skip (20).Take (20);

            Takee While
                numbers.TakeWhile (n => n < 100);
            Skip While
                numbers.SkipWhile (n => n < 100);
            
            
         */
        /*
            Distinct
            char[] distinctLetters = "HelloWorld".Distinct().ToArray();
            string s = new string (distinctLetters); // HeloWrd

            DistinctBy
            new[] { 1.0, 1.1, 2.0, 2.1, 3.0, 3.1 }.DistinctBy (n => Math.Round (n, 0))   //{1,2,3}
         */

        //Projecting
        //Select and SelectMany
        /*
            IEnumerable<FontFamily> query =
            from f in FontFamily.Families
            where f.IsStyleAvailable (FontStyle.Strikeout)
            select f;
            foreach (FontFamily ff in query) Console.WriteLine (ff.Name);

            //Projecting into annonymous object
            var query =
            from f in FontFamily.Families
            select new { f.Name, LineSpacing = f.GetLineSpacing (FontStyle.Bold) };
         */
        /*
        //Indexed Projection
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<string> query = names
            .Select ((s,i) => i + "=" + s); // { "0=Tom", "1=Dick", ... }

        //Projecting into concrette types
            from c in dbContext.Customers
            let highValueP = from p in c.Purchases
            where p.Price > 1000
            select new { p.Description, p.Price }
            where highValueP.Any()
            select new { c.Name, Purchases = highValueP };

            IQueryable<CustomerEntity> query =
            from c in dbContext.Customers
            select new CustomerEntity
            {
            Name = c.Name,
            Purchases =
            (from p in c.Purchases
            where p.Price > 1000
            select new PurchaseEntity {
            Description = p.Description,
            Value = p.Price
            }
            ).ToList()
            };

        //SelectMany
            "Anne", "Williams", "John", "Fred", "Smith", "Sue", Green"
            IEnumerable<string> query = fullNames.SelectMany (name => name.Split());

            IEnumerable<string> query =
            from fullName in fullNames
            from name in fullName.Split() // Translates to SelectMany
            select name;

            IEnumerable<string> query = fullNames
            .SelectMany (fName => fName.Split()
            .Select (name => new { name, fName } ))
            .OrderBy (x => x.fName)
            .ThenBy (x => x.name)
            .Select (x => x.name + " came from " + x.fName);

        //Cross Joun 

            var query = from c in dbContext.Customers
            from p in dbContext.Purchases
            where c.ID == p.CustomerID
            select c.Name + " bought a " + p.Description;
         */
    }
}
