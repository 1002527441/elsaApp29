using Esprima.Ast;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsaApp.Services
{
    public class Test
    {


        public Test()
        {
            var student = new Student("henry") { Lastname = "zhang" };

            var person = new Person("henry", "zhang");

            var std1 = student with { Firstname = "jack" };

            var std2 = student with { };


            var person2 = person with { Firstname = "jackma" };

            (var firstname, var lastname) = person;



        }


        public bool IsLetter(char c)
        {
            return c is >= 'a' and <='z' or >= 'A' and <='Z';
            return c is not( >= 'a' and <= 'z' or >= 'A' and <= 'Z');
        }


        public string classify(double temp) => temp switch
        {
            < 0 or <=30 => "tool cold",
            > 40 => "too hot",
            double.NaN => "Unknown",
            _ => "Just right",
        };


       





        public record Person(string Firstname, string LastName);

        public record Student(string Firstname)
        {
            public string Lastname { get; init; }
        }
    }
}
