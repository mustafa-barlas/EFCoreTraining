// resharper disable all

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;
using Generated_Values;
using Microsoft.EntityFrameworkCore;

ApplicationContext context = new ApplicationContext();


Product product = new Product();
Console.WriteLine(product.Id.ToString());


namespace Generated_Values
{

    public class Product
    {
        public string Id
        {
            get
            {
                return GenerateRandomId();
            }
            set
            {

                GenerateRandomId();
            }
        }

        public string Name { get; set; }


        static string GenerateRandomId()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] id = new char[12];
            for (int i = 0; i < id.Length; i++)
            {

                id[i] = characters[random.Next(characters.Length)];
            }
            return new string(id);
        }
    }

    


    #region Default Values

    #region HasDefaultValue



    #endregion

    #region HasDefaultValueSql



    #endregion

    #endregion

    #region Computed Columns



    #endregion

    #region Value Generation


    #region DatabaseGenerated

    #region DatabaseGeneratedOption

    // Identity oto olarak artan bir sütundur.



    public class Person1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]         // None Identity   ValueGeneratedNever
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //  Identity        ValueGeneratedOnAdd
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] //   Computed        ValueGeneratedOnAddOrUpdate
        public int Id { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Identity
        public int PersonCode { get; set; }
        public string? Name { get; set; }

    }


    public class ApplicationContext1 : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Person1>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Person1>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Person1>().Property(x => x.Id).ValueGeneratedOnAddOrUpdate();
        }
    }
    #endregion

    #endregion


    #endregion




    public class Person
    {

        public int Id { get; set; }

        public int PersonCode { get; set; }

        public string? Name { get; set; }

        public Address? Address { get; set; }
    }
    public class Address
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public Person? Person { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }


    }
}

