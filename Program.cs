using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static LiquCommand.ListGenerators;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Diagnostics.Metrics;
using System.Buffers.Text;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiquCommand
{
    public class Program
    {
        public static void Main(System.String[] args)
        {
            /// <LINQ>
            /// 
            /// LINQ, which stands for Language Integrated Query, is a powerful feature in C# that allows you
            ///    to write queries and perform operations on data directly within your code. 
            /// LINQ enables you to work with collections, databases, XML, and other data sources using a
            ///    consistent and expressive syntax.
            ///    
            /// liqu: language independent query.
            /// Use 40+ functions (Query Operator) Against Data, Regardless Data Store.
            ///             
            /// </LINQ>



            //---------------------------------------------------------------------------------------------//
            /// <Implicit_Typed_Local_Variable>
            /// when we use var to declar any data in c# this called Implicit typed local variable, which 
            ///   mean the compiler specify the data type depend on the default data that atteneded to variable
            ///   
            /// </Implicit_Typed_Local_Variable>
            var d = 124.213; // d is type of double here
            Console.WriteLine(d.GetType()); // output: System.Double
            // As C# is strong data type then we can't change data type at runtime.
            // d = "new data" // Execption as d can't implicit from double to string.

            //---------------------------------------------------------------------------------------------//
            ///<Extension_Method>
            ///</Extension_Method>
            // let's consider the following example where we have an integer and want to reverse it:
            int x = 12345;
            WriteLine(int32Extension.mirror(x)); // output: 54321

            // Now if we need to call the mirror function which is in int32Extension class as a member function
            // x.mirror(); this can't be vaild as mirror function is not a member function
            // but to do so we need to make this method act as a member Method


            // to make method act as member method there are some conditions that we have to follow:
            // 1. we have to make this method accept the same object by passing this which refere to the same
            //     object to the method
            // 2. this method must be in a static class as the method that accept this could be only in the 
            //      static calss.
            // This method will called the extension method which must be in non genric static class.
            // so we create new class with name int32ExtensionV2.
            WriteLine(x.mirror());// output: 54321

            //--------------------------------------------------------------------------------------------//
            /// <Anonymous_Type>
            /// 
            /// Anonymous types in C# allow you to create objects with properties without defining a named 
            ///  class type explicitly. They are particularly useful when you need to project data from a data
            ///  source, such as a database or LINQ query, into a new type without the need to define a formal class.
            /// Here are the key characteristics and examples of anonymous types in C#:
            /// 
            /// Properties: Anonymous types consist of a set of read-only properties.
            /// Each property is associated with a value,and you can access these properties using dot notation
            ///  
            /// Implicitly Typed: The type of an anonymous type is inferred by the compiler. 
            ///  You don't specify a type explicitly; instead, the compiler generates a type behind the scenes.
            /// 
            /// Read - Only: The properties of an anonymous type are read-only.
            ///   Once set, their values cannot be changed.
            /// 
            /// To create an anonymous type we need to use var as a type for it.
            /// var anonymousObject = new { Property1 = value1, Property2 = value2, ... };
            /// 
            /// As we know the way to create an anonymous method to use it with delegate, now we could create 
            ///   Anonymous type which mean anonymous class
            /// 
            /// </Anonymous_Type>
            
            // All Anonymous type are Imutable type.

            var Employee1 = new { Id = 1, Name = "Mahmoud", Salary = 12000 };
            WriteLine(Employee1);

            // Employee1.ID = 2; 
            // This line is not valid as the property of Anonymous type is read only, so we can't change it.

            // if we create another object with the same property name and same data type, it will be object 
            //   of the same type.
            var Employee2 = new { Id = 2, Name = "Ibrahim", Salary = 5000 };
            // Same type as long as same property(With the same Charcter case), Same propeerty, and same sequence.

            var Employee3 = new { Id = 2, Salary = 5000, Name = "Ibrahim" };
            // new Type as the sequence of property are not the same.

            var Employee4 = new { Id = 1, Name = "Mahmoud", Salary = 12000 };
            // If we try to check equality between two object of the same anonymous type we will find that 
            //   the equality is based on value equality like we make an override to equal.
            if (Employee1.Equals(Employee4))
                WriteLine("Equals"); // output: Equals 
            // as they have the same value type for property.

            
            // If we try to check hashcode for Employee1 and Employee4 then we will find them same as each 
            //  other, as Anonymous type override getHasCode basd on value not Identity.
            WriteLine(Employee1.GetHashCode());
            WriteLine(Employee4.GetHashCode());

            //---------------------------------------------------------------------------------------------//
            /// <Linq_Query>
            /// linq Queries against any Sequence.
            /// Sequence: any Class Implementing IEnumberable<T> interface.
            /// Local Sequence: L2Object, L2ADO, L2XML.
            /// Remote Sequence: L2Sql, L2EF.
            /// Sequence Contains elements.
            /// </Linq_Query>
            List<int> intLst = new List<int>() { 1, 6, 7, 8, 9, 12, 17, 5};
            List<System.String> NameLst = new List<string> { "Mahmoud", "Ahmed", "Emad", "Omer", "Saadallah" };
            //--------------------------------------------------------------------------------------------//
            /// <Where>
            /// 
            /// In LINQ (Language Integrated Query), the Where operator is used to filter elements from a 
            ///   collection or data source based on a specified condition. 
            /// It allows you to retrieve only the elements that meet a specific criteria or satisfy 
            ///   a given predicate
            ///   
            /// Syntax:
            /// IEnumerable<T> filteredCollection = sourceCollection.Where(element => condition);
            /// 
            /// sourceCollection: The original collection or data source.
            /// element: A placeholder variable representing each element in the collection.
            /// condition: A Boolean expression that defines the filtering criteria.
            /// 
            /// </Where>

            // we call where here as static function in Enumerable class.
            // It take here two input parameters Collection or source of data and Boolean condition.
            var Result = Enumerable.Where(intLst, i => i % 2 == 0);
            var Result1 = Enumerable.Where(NameLst, i => i.Length > 5);
            IEnumerable<int> Result2 = Enumerable.Where(intLst, i => i % 3 == 0);
            // We could use both var or IEnumerable to get the result from where, but when we use IEnumerable 
            //   then we have to specify the data type, but with var we don't need to specify data type.


            // We could use where as Extension Method without using Enumerable class with collections
            // In this case the where will take single parameter which is boolean Condition.
            var Result3 = NameLst.Where(i => i.Length > 5);

            // these two ways(Enumberable Class, Extension Method) called Fluent Syntax.

            // There is another way to the same task, but now it will be look like Sql Query.
            // Query Expression, or Query Syntax.
            var Result4 = from i in NameLst
                          where i.Length > 5
                          select i;
            // Sql like style valid for only subset of (40+ Linq Operators).
            // It will be easy to use this way with (Join, Group, Let, Into).
            // Start with From, introduce Range Variable (i):represent each and every element in Input Sequence
            // End with select or Group By.

            // If we want ot know the type of var in the prev examples we will find it WhereListIterator type.
            WriteLine(Result4.GetType()); // output: WhereListIterator
            // To change this type to any other collection type we could make a cast to the left side using:
            // ToList(), ToArray, ToDictionary, ToHashSet or even ToString.
            var Result5 = NameLst.Where(i => i.Length > 5).ToList();
            WriteLine(Result5.GetType()); // output: Generic.List
            
            foreach (string i in Result5)
                 WriteLine(i);// output: Mhmoud   Saadallh.

            // There is another feature working with Linq which is:
            // Most of LINQ Operators, Deffered Execution which mean when we want ot get data from one of 
            // Linq operators the query will be execute again to get the last version of data.

            NameLst.AddRange(new string[] { "Ibrahim", "Mostafa", "said" });
            foreach(var i in Result4)
                Write(i + ", "); // output: Mahmoud, Saadallah, Ibrahim, Mostafa,
            // As we see Although the Result4 was declared and inialized before addingRange to NameLst
            //   but the when we want to get data form Result4 the enging Rexecute Result4  to get the last
            //   version of data.
            // We have to know that this feature could be cancled if we cast the var result4 ToList.
            // like in Result5 if we want to print data from it, it will not get the last version 
            //  as Result5 was casted to list.
            WriteLine();

            //-------------------------------------//
            // now let's do the same with listGEnerators class.
            WriteLine(ProductList[0]);

            var Result6 = ProductList.Where(i => i.UnitsInStock == 0);
            foreach (var i in Result6)
                Write(i.ProductName + ", "); // output: Chef Anton's Gumbo Mix, Alice Mutton, Thüringer
                                             //         Rostbratwurst, Gorgonzola Telino, Perth Pasties,

            // We could also use mulit conditions with the same where by two ways:
            // 1. using && operator
            Result6 = ProductList.Where(i => (i.UnitsInStock == 0) && (i.Category == "Meat/Poultry"));
            foreach (var i in Result6)
                Write(i.ProductName + ", "); // output: Alice Mutton, Thüringer Rostbratwurst, Perth Pasties,

            // 2. By using two laryer of filteration.
            Result6 = ProductList.Where(i => i.UnitsInStock == 0).Where(i => i.Category == "Meat/Poultry");
            foreach (var i in Result6)
                Write(i.ProductName + ", "); // output: Alice Mutton, Thüringer Rostbratwurst, Perth Pasties,

            ///<Index_Where>
            /// There are another overLoad for where which accept two paramters condition and index number
            ///  and return bool.
            /// </Index_Where>
            Result6 = ProductList.Where((p, i) => p.UnitsInStock == 0 && i < 10);
            // This line of code mean all product that unitsInStock equal zero and the index of this product 
            //   is in the first ten products in the list.
            // We have to know that Index Where vaild only with Fluent Syntax. 
            // Can't be Written using Query Exerperssion.

            //-----------------------------------------------------------------------------------------------//
            ///<Select>
            ///
            /// the Select operator is used to project or transform elements from a collection or data source 
            ///   into a new form.
            /// It allows you to create a new sequence of values based on some computation or transformation
            ///   applied to each element in the source collection
            /// Syntax:
            /// IEnumerable<TResult> projection = sourceCollection.Select(element => transformation);
            /// 
            /// sourceCollection: The original collection or data source.
            /// element: A placeholder variable representing each element in the source collection.
            /// transformation: An expression that defines how each element should be transformed.
            /// 
            ///</Select>

            var Result7 = ProductList.Select(i => i.ProductName);
            foreach (var i in Result7)
                Write (i + ",  ");
            // we could also use Query experssion
            Result7 = from p in ProductList
                      select p.ProductName;
            foreach (var i in Result7)
                Write(i + ",  ");
            //Here we select all product name and we could transform them to new collection or new storage type
            // we have to know that the enging know that Result7 is type of IEnumerable of String.
            // So in the previous Examples we transform form user defined type(List) to bulit in data type(string)
            // Product => string.

            // We could use it to transform to Anonymous type.
            var Result8 = ProductList.Select(p => new { Name = p.ProductName });
            // In this example by using new keyword we create a new Anonymous type.
            // We could also do the same with query expression
            Result8 = from p in ProductList
                      select new { Name = p.ProductName };
            // Product => Anonymous type.
            //
            // Let's see how we could use select with Anonymous type in real world 
            // consider that we have a new discount to all our product or some specific product and 
            // we want to apply this discount or we want to transform the discounted product to new list or
            // to new Anonymous type to apply discount we could use the following
            var DiscountedLst = ProductList.Select(p => new
            {
                ProductName = p.ProductName,
                p.ProductID,
                p.Category,
                UnitPrice = p.UnitPrice * 0.9M
            });
            // In this example we change UnitPrice from its value to 0.9 of its value with discount 10%
            // We also use the productID, and Category without assign any of them to new property which mean 
            //   we need these property with the same name in this New anonymous type.

            // As there is an Indexted where there is also an Indexted Select
            ///<Index_Select>
            /// There are another overLoad for Select which accept two paramters condition and index number
            ///  and return bool.
            /// Index select vaild only with Fluent Syntax.
            /// </Index_Select>
           var Result9 = ProductList.Select((p, i) => new
            {
                Index = i,
                ProductName = p.ProductName
            });

            //-----------------------------------------------------------------------------------------------//
            /// <Ordering_Elements>
            /// 
            /// the orderby operator is used to sort elements from a collection or data source based on one or
            ///   more specified criteria.
            /// It allows you to order the elements in ascending or descending order according to the values
            ///   of one or more properties. 
            /// Syntax:
            /// IEnumerable<TSource> orderedCollection = sourceCollection.OrderBy(element => keySelector);
            /// sourceCollection: The original collection or data source.
            /// element: A placeholder variable representing each element in the source collection.
            /// keySelector: An expression that specifies the property or value by which to sort the element
            /// 
            /// </Ordering_Elements>
            var Result10 = ProductList.OrderBy(p => p.UnitsInStock);

            Result10 = from p in ProductList
                       orderby p.UnitsInStock
                       select p;
            // The Qurey experession must end with select.

            // We could use chain of Linq operators.
            var Result11 = ProductList.OrderBy(p => p.UnitsInStock)
                .Select(i => (i.ProductName, i.ProductID)).Where(p => p.ProductID > 7);
            // In this example we use a chain of Linq operators, but we have to know the in input of any 
            //   operator is the output of the previous operator.
            // Like in our example the input of select operator is the outpur of orderby Operator and the 
            //    input of where operator is the output of select operator.

            // The default of ordering is Ascending, we could use orderbyDescending to make the ordrring in 
            //   descending order.
            Result11 = ProductList.OrderByDescending(p => p.UnitsInStock)
                .Select(i => (i.ProductName, i.ProductID)).Where(p => p.ProductID > 7); // descending order.


            // We could order by two things but in this way we have to use .ThenBy in the flunt way 
            //  and use the same we use in sql query in the query expression
             Result10 = ProductList.OrderBy(p => p.UnitsInStock).ThenBy(p => p.UnitPrice); // using ThenBy

            Result10 = from p in ProductList
                       orderby p.UnitsInStock, p.UnitPrice  // just the same as in sql
                       select p;
            // There is also ThenBy Descending.
            Result10 = ProductList.OrderBy(p => p.UnitsInStock).ThenByDescending(p => p.UnitPrice); // using ThenBy

            Result10 = from p in ProductList
                       orderby p.UnitsInStock ascending, p.UnitPrice descending // just the same as in sql
                       select p;

            //-----------------------------------------------------------------------------------------------//
            /// <Element_Operators>
            /// 
            /// Element operators in LINQ are a set of operators that allow you to retrieve a single element
            ///   from a collection or sequence based on specific criteria.
            /// These operators are useful when you need to find an element that meets certain conditions or
            ///   when you want to retrieve the first, last, or any element from a sequence.
            ///   
            /// We have to know that the Element Operators are vaild only with Fluent Expression, so we can't 
            ///   use them with query expression.
            /// 
            /// 1. First: Returns the first element from a sequence that satisfies a specified condition.
            ///     If no matching element is found, an exception is thrown.
            ///     var firstEven = numbers.First(n => n % 2 == 0);
            ///     
            /// 2. FirstOrDefault: Returns the first element from a sequence that satisfies a specified 
            ///    condition or a default value if no matching element is found. 
            ///    This operator is useful when you want to avoid exceptions.
            ///    var firstEven = numbers.FirstOrDefault(n => n % 2 == 0);
            /// 
            /// 3. Single: Returns the only element from a sequence that satisfies a specified condition.
            ///     If there is more than one matching element or no matching element, an exception is thrown.
            ///     var singleEven = numbers.Single(n => n % 2 == 0);
            ///     
            /// 4. SingleOrDefault: Returns the only element from a sequence that satisfies a specified
            ///     condition or a default value if there is no matching element. 
            ///     If there are multiple matching elements, an exception is thrown.
            ///     var singleEven = numbers.SingleOrDefault(n => n % 2 == 0);
            ///     
            /// 5. Last: Returns the last element from a sequence that satisfies a specified condition. 
            ///     If no matching element is found, an exception is thrown.
            ///     var lastEven = numbers.Last(n => n % 2 == 0);
            /// 
            /// 6. LastOrDefault: Returns the last element from a sequence that satisfies a specified condition
            ///     or a default value if no matching element is found. 
            ///     This operator is useful when you want to avoid exceptions.
            ///     var lastEven = numbers.LastOrDefault(n => n % 2 == 0);
            ///     
            /// 7. ElementAt: Returns the element at a specified index in a sequence. 
            ///    If the index is out of range, an exception is thrown.
            ///    var thirdElement = numbers.ElementAt(2);
            ///    
            /// 8. ElementAtOrDefault: Returns the element at a specified index in a sequence or a default 
            ///    value if the index is out of range.
            ///    var thirdElement = numbers.ElementAtOrDefault(2);
            ///  
            /// </Element_Operators>
            var single = ProductList.First();
            WriteLine(single);

            single = ProductList.FirstOrDefault(p => p.UnitPrice % 5 == 0);
            WriteLine(single);

            single = ProductList.Single(p => p.UnitPrice % 2 == 0); // Execption
            WriteLine(single);

            single = ProductList.Single(p => p.ProductID == 7); // no Execption as it the only one result.
            WriteLine(single);

            single = ProductList.SingleOrDefault(p => p.UnitPrice % 2 == 0); // Execption
            WriteLine(single);

            single = ProductList.Last();
            single = ProductList.Last(p => p.UnitPrice > 50);
            WriteLine(single);

            single = ProductList.LastOrDefault();
            single = ProductList.LastOrDefault(p => p.UnitPrice > 50);
            WriteLine(single);


            single = ProductList.ElementAt(7);
            WriteLine(single);

            single = ProductList.ElementAtOrDefault(9);
            WriteLine(single);

            // It's better to use FirstOrDefault, LastOrDefault, ElementAtOrDefault, SingleOrDefault
            //    to avoid Exepetions.

            // We could use Hybrid Syntax which is a compossion between Flunet and query 
            var single2 = (from p in ProductList
                     where p.UnitsInStock == 0
                     select new {p.ProductName, p.UnitPrice}).First();
            // We have to know that when we use Hybrid syntax we have to put the query between two circler 
            //    brackets then use dot "." with Fluent syntax.

            //-----------------------------------------------------------------------------------------------//
            /// <Aggregate_Operators>
            /// 
            /// Aggregate operators in LINQ are used to perform cumulative or iterative operations on elements
            ///    in a collection or sequence. 
            /// These operators combine the elements of a collection into a single result by applying a
            ///    specified operation or function to each element iteratively.
            /// Here are some commonly used aggregate operators in LINQ:
            /// 
            /// 1. **`Count`**: Returns the number of elements in a collection that satisfy a specified 
            ///      condition.   
            ///    int count = numbers.Count(n => n % 2 == 0);   
            ///    
            /// 2. **`Sum`**: Computes the sum of all numeric values in a collection.   
            ///    int sum = numbers.Sum();  
            ///    
            /// 3. **`Min`**: Finds the minimum value in a collection.
            ///    int min = numbers.Min(); 
            ///    
            /// 4. **`Max`**: Finds the maximum value in a collection.  
            ///    int max = numbers.Max();  
            ///    
            /// 5. **`Average`**: Computes the average(mean) of all numeric values in a collection.
            ///     double average = numbers.Average();
            ///     
            /// 6. **`Aggregate`**: Applies a custom aggregation function to a collection. This operator 
            ///    allows you to perform more complex aggregation operations by specifying a custom function.
            ///      int product = numbers.Aggregate((acc, next) => acc * next); 
            ///      
            /// 7. **`Count` (without a condition)**: Returns the total number of elements in a collection.
            ///    int total = numbers.Count();  
            ///    
            /// 8. **`All`**: Checks if all elements in a collection satisfy a specified condition.  
            ///    bool allEven = numbers.All(n => n % 2 == 0); 
            ///    
            /// 9. **`Any`**: Checks if any elements in a collection satisfy a specified condition.  
            ///    bool anyEven = numbers.Any(n => n % 2 == 0);  
            ///    
            /// 10. **`First`**: Returns the first element in a collection that satisfies a specified
            ///     condition.If no matching element is found, an exception is thrown.  
            ///     int firstEven = numbers.First(n => n % 2 == 0);  
            ///     
            /// 11. **`FirstOrDefault`**: Returns the first element in a collection that satisfies a specified
            ///     condition or a default value if no matching element is found.This operator is useful
            ///     when you want to avoid exceptions.    
            ///     int firstEven = numbers.FirstOrDefault(n => n % 2 == 0);
            ///     
            /// These aggregate operators are useful for performing various calculations and summary
            ///   operations on data in collections or sequences.
            /// They help simplify code and make it more expressive when working with data that needs to be
            ///    aggregated or summarized.
            ///    
            /// </Aggregate_Operators>

            var agg = ProductList.Count(); // count without condition Return the total number of element in collection.
            agg = ProductList.Count(p => p.UnitPrice > 50);
            WriteLine(agg);

            agg = (int)ProductList.Sum(p => p.UnitPrice);
            WriteLine(agg);

            agg = (int)ProductList.Min(p => p.UnitPrice);
            WriteLine(agg);

            agg = (int)ProductList.Max(p => p.UnitPrice);
            WriteLine(agg);
            // If we use Max without any Predicite condition then we have first to empliment ICompareable.

            agg = (int)ProductList.Average(p => p.UnitPrice);
            WriteLine(agg);

            var agg2 = ProductList.All(p => p.UnitPrice % 2 == 0);
            WriteLine(agg2);


            //-----------------------------------------------------------------------------------------------//
            /// <Generators_Operators>
            /// Only way to call them is as static Member from Enumerable class.
            /// Generator operators in LINQ are a set of operators that allow you to generate sequences of 
            ///    values based on specified patterns or rules.
            /// These operators produce sequences of elements on-the-fly rather than operating on existing
            ///    collections or data sources.
            /// Generator operators are useful when you need to generate sequences of data for various 
            ///   purposes.
            /// Here are some commonly used generator operators in LINQ:
            /// 
            /// 1.`Range`: Generates a sequence of consecutive integers within a specified range.
            ///    var numbers = Enumerable.Range(1, 5); // Generates 1, 2, 3, 4, 5
            ///    
            /// 2.`Repeat`: Generates a sequence that repeats a specified value a specified number of times.
            ///   var repeatedValues = Enumerable.Repeat("Hello", 3); // Generates "Hello", "Hello", "Hello"\
            ///   
            /// 3.`Empty`: Generates an empty sequence with no elements.
            ///   var emptySequence = Enumerable.Empty<int>(); // Generates an empty sequence 
            ///   
            /// 4.`DefaultIfEmpty`: Generates a sequence with a single default value if the source sequence is empty.  
            ///   var numbers = new List<int>();
            ///   var result = numbers.DefaultIfEmpty(0); // Generates 0 if 'numbers' is empty   
            ///   
            /// 5.`Generate`: Generates a sequence of values based on a custom generation function.You provide
            ///   a function that generates the next element based on the previous element.  
            ///   var fibonacci = Enumerable.Generate(
            ///        seed: (1, 1),
            ///        condition: pair => pair.Item1 <= 100,
            ///        iterate: pair => (pair.Item2, pair.Item1 + pair.Item2),
            ///        resultSelector: pair => pair.Item1
            ///    ); // Generates the Fibonacci sequence up to 100   
            ///    
            /// 6.`ToLookup`: Generates a lookup table from a sequence by grouping elements based on a key selector.   
            ///    var lookup = numbers.ToLookup(n => n % 2 == 0); // Groups even and odd numbers  
            ///    
            /// 7.`ToDictionary`: Generates a dictionary from a sequence using key and value selectors.  
            ///    var dict = persons.ToDictionary(person => person.Id, person => person.Name); // Generates a dictionary of persons by Id  
            ///    
            /// Generator operators are helpful when you need to create sequences of data that follow specific
            ///   patterns or rules, or when you want to transform data from one format into another, 
            ///   such as turning a sequence into a dictionary or lookup table. 
            /// They provide flexibility and convenience for generating data on - the - fly in your LINQ 
            ///   queries and applications.
            ///   
            /// </Generators_Operators>

            // 1.Range( , ) function make an IEnumerable of integer that has specific range.
            var Gen = Enumerable.Range(0, 10);

            // 2.Empty<>() function which make an empty of IEnumerable of specific type.
            var Gen2 = Enumerable.Empty<Product>();

            // 3.Repeat( , ) function the Repeat specific value to number of times.
            var Gen3 = Enumerable.Repeat(3, 10);
            var gen3 = Enumerable.Repeat("Hello", 10);
            // this example make an IEnumerable that contain 3 for ten times.
            // We have to know that if we use Repeat with Reference type this mean we don't create multiple 
            //   object of this type, but we just make a refernecet to this object.
            var Gen4 = Enumerable.Repeat(ProductList[3], 5);
            foreach (var p in Gen4)
                WriteLine(p);
            // now if we change the value of ProuctList[3] the Gen4  IEnumerable will be changed to the new 
            //   value
            ProductList[3].ProductName = "Temp";
            foreach (var p in Gen4)
                WriteLine(p);
            // now all the porduct in the Gen4 will have the name Temp as the ProductName.

            // 4.DefaultIfEmpty: Generates a sequence with a single default value if the source sequence is empty.
            var Gen5 = Enumerable.Range(5, 7);
            var Gen6 = Gen5.DefaultIfEmpty(0);
            // The first line will create an IEnumerable that contain Range of Integer form 5 to 7 
            // The second line check if the IEnumerable Gen5 is Empty or not, if it's Empty then it will
            //   generat a sequence with singel default value which is zero.

            //5.ToLookup:Generates a lookup table from a sequence by grouping elements based on a key selector.
            // The ToLookup operator in LINQ is used to generate a lookup table (a collection of key-value pairs)
            // from a sequence of elements based on a specified key selector. It's similar to the GroupBy
            // operator but creates a data structure optimized for efficient retrieval of grouped elements.
            var lookup = Gen5.ToLookup(n => n % 2 == 0); // Groups even and odd numbers


            // 6.ToDictionary: Generates a dictionary from a sequence using key and value selectors.
            var Gen7 = Gen.ToDictionary(n => n % 2 == 2);

            //---------------------------------------------------------------------------------------------//
            /// <Select_Many>
            /// 
            /// The SelectMany operator in LINQ is used to project and flatten elements from nested collections
            ///   or sequences into a single, flattened sequence.
            /// It's particularly useful when you have a collection of collections (or a nested structure) and
            ///   you want to work with the individual elements inside those nested collections as if they
            ///   were all part of a single flat collection.
            /// 
            /// Return Type:
            /// IEnumerable<TResult>: This is the resulting flattened sequence containing the elements
            ///    from the nested collections.
            
            /// </Select_Many>
            List<string> strlst = new List<string> { "Ahmed Mohammed", "Mahmoud Saadallah", "Ibrahim Osman" };
            var Selector = strlst.SelectMany(s => s.Split(" "));
            // We could do the same using Query exepression
            var Selector2 = from name in strlst
                            from s in name.Split(" ")
                            select s;

            //--------------------------------------------------------------------------------------------//
            /// <Set_Operators>
            /// 
            /// Set operators in LINQ are used to perform set-related operations on collections or sequences. 
            /// These operators allow you to manipulate collections as sets, taking into account concepts like 
            ///   union, intersection, difference, and distinct values. 
            /// Here are some commonly used set operators in LINQ:
            /// 
            /// 1.Union: Returns a new collection that contains all distinct elements from both source collections.
            /// var unionResult = collection1.Union(collection2);
            /// 
            /// 2.Intersect: Returns a new collection that contains the elements that exist in both source collections.
            /// var intersectResult = collection1.Intersect(collection2);
            /// 
            /// 3.Except: Returns a new collection that contains the elements that exist in the first 
            ///   collection but not in the second collection.
            ///   var exceptResult = collection1.Except(collection2);
            ///   
            /// 4.Distinct: Returns a new collection with distinct elements from the source collection
            /// var distinctResult = collection.Distinct();
            /// 
            /// 5.Concat: Returns a new collection that contains all elements from both source collections, 
            ///   preserving the order.
            ///   var concatResult = collection1.Concat(collection2);
            ///   
            /// 6.SequenceEqual: Determines if two collections have the same elements in the same order.
            /// bool areEqual = collection1.SequenceEqual(collection2);
            /// 
            /// </Set_Operators>

            // Before we take an examples about Set Operators We have to know some feature about array and list
            var lst = Enumerable.Range(0, 100); // create a range from 0 to 99
            var lst2 = Enumerable.Range(50, 100); // create a range from 50 to 149
            // So we have to know that the range accept the start point and the counter not the end of the range
            var lst3 = lst.ToList();
            WriteLine(lst3[0]); // 0
            WriteLine(lst3[^1]); // this will Return the last index which is 99
            WriteLine(lst3[^5]); // this will Return the element number five from the end which is 95
            List<int> lst4 = lst3[1..10];
            // This will Return a list of indexes in lst3 start from index one to Index 10
            // It's exactly like taking a range of indexes form list.
            // This also could be used with array.

            var seq1 = Enumerable.Range(0, 100); // create a range from 0 to 99
            var seq2 = Enumerable.Range(50, 100); // 50 - 149

            var output = seq1.Union(seq2); //  Returns a new collection that contains all distinct elements
                                           //  from both source collections.
                                           //  This mean it's remove Duplicate. 0 - 149
            // Union has another overloaded that accept the way of comparing like we want to remove duplicate 
            //   depend on the Id, or any thing else.

            output = seq1.Concat(seq2); // This will allow the Duplication.

            output = output.Distinct(); // Delate the Duplicate values.

            output = seq1.Except(seq2); // This will return indexes that in the seq1 and not in seq2. 0-49

            output = seq1.Intersect(seq2); // This will return indexes that common between seq1 and seq2. 50-99


            //---------------------------------------------------------------------------------------------//
            /// <Casting_Operators>
            ///
            /// Casting in Linq could be in a deffrent ways we could use 
            /// 1.ToList(): To cast to list.
            /// 2.ToArray(): To cast to array.
            /// 3.ToDictionary(): To cast to Dictionary, but we have to know there are four overloaded method 
            ///    to use this cast:
            ///    The First: that accept key selector.
            ///    The Second: that accept key selector and comparer.
            ///    The Third: that accept key selector, and Element Selector.
            ///    The fourth: that accept key selector, Element Selector, and comparer
            /// 4.ToHashSet: To cast to Hashset which has two overloaded method
            ///    we have to know that Hashset like array but with unique values.
            ///    The First: that doesn't accept any thing.
            ///    The Second: that accept Comparer.
            /// 5. ToLookup: to cast to lookup which has four overloaded method which are the same as 
            ///    Dicitionary 
            /// 
            /// </Casting_Operators>

            Product[] casting1 = ProductList.Where(p => p.UnitsInStock == 0).ToArray();
            List<Product> casting2 = ProductList.Where(p => p.UnitsInStock == 0).ToList();
            var casting3 = ProductList.Where(p => p.UnitsInStock == 0).ToHashSet();
            var casting4 = ProductList.Where(p => p.UnitsInStock == 0).ToDictionary(p => p.ProductID);
            var casting5 = ProductList.Where(p => p.UnitsInStock == 0).ToLookup(p => p.ProductID);

            //----------------------------------------------------------------------------------------------//
            /// <Quantifiers_Operators>
            ///
            /// In LINQ (Language Integrated Query), quantifiers are operators that determine whether certain 
            ///   elements in a collection satisfy a specific condition. 
            /// Quantifiers return a Boolean value (true or false) based on whether the condition is met by any
            ///   or all elements in the collection. 
            /// There are two main quantifier operators in LINQ: Any and All.
            /// 
            /// 1.Any Operator:
            /// The Any operator checks whether there are any elements in a collection that satisfy a 
            ///   specified condition.
            /// It returns true if at least one element meets the condition; otherwise, it returns false.
            /// The condition is defined using a lambda expression or a delegate.
            /// List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            /// bool hasEvenNumbers = numbers.Any(num => num % 2 == 0); // true (at least one even number)
            /// bool hasNegativeNumbers = numbers.Any(num => num < 0);   // false (no negative numbers)
            /// 
            /// 2.All Operator:
            /// The All operator checks whether all elements in a collection satisfy a specified condition.
            /// It returns true if every element in the collection meets the condition; otherwise, it returns false.
            /// The condition is defined using a lambda expression or a delegate.
            /// List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };   
            /// bool allPositiveNumbers = numbers.All(num => num > 0);    // true (all numbers are positive)
            /// bool allEvenNumbers = numbers.All(num => num % 2 == 0);  // false (not all numbers are even)
            /// 
            /// </Quantifiers_Operators>
            List<int> intlst = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            WriteLine(intlst.Any(n => n % 2 == 0)); // Output: Ture
                                                    // As there is at least one element Satisfy the condition.
            WriteLine(intlst.Any(n => n < 0)); // Output: False
                                               // As there is no element staisy the condition.
            WriteLine(intlst.All(n => n > 0)); // Output: True
                                               // As the all elements in the list satisfy with the condition.
            WriteLine(intLst.All(n => n % 2 == 0)); // Ouput: False
                                                  // As Not All Elements in the list satisfy with the condition

            // If we use the Any operator without condition, this mean we check if the collection contain at 
            //  least one element.
            WriteLine(intlst.Any()); // Output: Ture As there is at least one element is the list.

            //---------------------------------------------------------------------------------------------//
            ///<Zip_Operator>
            ///
            /// The Zip operator in LINQ is used to combine elements from two sequences (collections) into a
            ///   single sequence, element by element, based on their position.
            /// It creates pairs of elements from the input sequences where the first element in each pair
            ///  comes from the first sequence, and the second element comes from the second sequence,
            ///  both having the same index.
            ///  
            /// The Zip operator continues processing until it reaches the end of either of the input 
            ///   sequences, so the length of the resulting sequence is determined by the shorter of
            ///   the two input sequences.
            ///   
            /// </Zip_Operator>
            List<int> number = new List<int> { 1, 2, 3, 4 };
            List<string> words = new List<string> { "One", "Two", "Three" };
            var zipped = number.Zip(words);
            foreach(var i in zipped)
                Write(i); // (1, One)(2, Two)(3, Three)
            /*
             * In this example, numbers1 and words are zipped together to create a sequence of strings 
             *  where each element combines an integer from numbers1 with a string from words. 
             * The resulting sequence will only contain three elements because it stops when it 
             *  reaches the end of the shorter input sequence (words in this case).
             */

            // We have to know that the Zip method has three overloaded the first one which accept one
            //  parameter the second sequance of collection like what we use.
            // The second one which accept two parameters which are second collection and ouput ruslt selector.
            var zipped2 = number.Zip(words, (n, name) => new {n, Names = name.ToUpper()});
            foreach(var i in zipped2)
                Write(i); // { n = 1, Name = ONE }{ n = 2, Name = TWO }{ n = 3, Name = THREE }

            // The Third one which accept Two parameters which are second collection and the third collection
            //  to be zipped.
            List<char> ch = new List<char>() { '!', '@', '#', '$', '%' };
            var zipped3 = number.Zip(words, ch);
            foreach (var i in zipped3)
                Write(i); // (1, One, !)(2, Two, @)(3, Three, #)


            //-----------------------------------------------------------------------------------------------//
            /// <Grouping>
            ///
            /// In LINQ (Language Integrated Query), the GroupBy operator is used to group elements from a 
            ///   collection based on one or more key criteria.
            /// This operator allows you to organize data into groups, and each group contains elements that
            ///   share a common key or set of keys.
            /// The result is typically an enumerable of groups, where each group has a key and contains a
            ///   collection of elements that match that key.
            ///   
            /// Key points to remember about GroupBy:
            /// 
            /// The result of GroupBy is an enumerable sequence of groups, and you can iterate through these 
            ///    groups to access the elements within each group.
            /// You can use multiple key criteria by providing a more complex keySelector lambda expression.
            /// You can perform various operations on the grouped data, such as aggregations, filtering, and 
            ///    rojections, using other LINQ operators after the GroupBy operation.
            /// GroupBy is a powerful operator for organizing and aggregating data based on specific criteria,
            ///    making it a valuable tool in LINQ queries for tasks like data summarization and reporting.
            ///   
            /// </Grouping>


            // As we know we could write our linq command in a Query style so we could use group that used in
            //   query.
            var items = from p in ProductList
                        where p.UnitsInStock > 0
                        group p by p.Category;

            foreach (var proGroup in items)
                WriteLine(proGroup.Key);// Beverages
                                        // Condiments
                                        // Produce
                                        // Meat / Poultry
                                        // Seafood
                                        // Dairy Products
                                        // Confections
                                        // Grains / Cereals

            /// Here we use group by to make the result of the query as groups not in indvioule items, but in 
            ///  groups, and we use proGroup.key to get name of groups as we can't access the name of groups
            ///  without the key feild.
            /// We could also modify this query to add some condition in groups not in products before goruping
            ///   them, but we have to use into to save groups.
            items = from p in ProductList
                    where p.UnitsInStock > 0
                    group p by p.Category
                    into Groups
                    where Groups.Count() > 10
                    orderby Groups.Count() descending
                    select Groups;
            foreach (var proGroup in items)
                WriteLine(proGroup.Key); // Confections
                                         // Beverages
                                         // Seafood
                                         // Condiments

        
            // We could also make the same grouping using the Fluent sentance using IEnumerable GroupBy
            items = ProductList.GroupBy(p => p.Category).Where(p => p.Count() > 10)
                .OrderByDescending(p => p.Count());
            foreach (var proGroup in items)
                WriteLine(proGroup.Key); // Confections
                                         // Beverages
                                         // Seafood
                                         // Condiments

            // We get the same result.


            //----------------------------------------------------------------------------------------------//
            ///<Let_and_Into>
            ///</Let_and_Into>

            List<string> names = new List<string>(){ "Mahmoud", "Aly", "Osman", "Sally", "Shrouk", "Omar" };
            // Let's consider this situation where we have a list of string that we need to delete all the 
            //  vowels letters from each string after that we need to delete all the strings that will 
            //  have length less than 4 using query exeperssion.
            var Novowels = from n in names
                           select Regex.Replace(n, "[aeiouAEIOU]", string.Empty)
                           // Now if we want to delete the strings that have legth less than 5 we need to use
                           //   where to filter the output of select, but we can't use where after the select 
                           //   as the select or group must be the last thing in quey exepression so we have 
                           //   to start from the beging. To do so we have to use Into to make the query start
                           //   from new point.
                           // We have to now that when using into the old range variable(n) is not
                           //   accessable after into so we can't use the variable n after the into.
                           into deletedVowels
                           where deletedVowels.Length >= 4
                           select deletedVowels;
            // Now after using into we make the query start again from this point.
            foreach (var n in Novowels)
                Write(n + "  "); // Mhmd  Slly  Shrk


            // now if we need to use the old range variable and also the new range variable, then we have to
            //  us let instead of using select.
            Novowels = from n in names
                       let deletedVowels = Regex.Replace(n, "[aeiouAEIOU]", string.Empty)
                       // Now after using let instead of using select we could contionue witout using into 
                       // and also the old rang variables are accessable.
                       where deletedVowels.Length >= 4
                       orderby n.Length // here we use the old range variable which is valid with let.
                       select deletedVowels; 

            // Let and Into are useing only with query exepression.

        }// End of main
    }
    class int32Extension
    {
        public static int mirror(int x) 
        {
            string s = x.ToString();
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return int.Parse(new string(charArray));
        }
    }
    static class int32ExtensionV2
    { 
        // Extension Method.
        public static int mirror( this int x)
        {
            string s = x.ToString();
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return int.Parse(new string(charArray));
        }
    }
}