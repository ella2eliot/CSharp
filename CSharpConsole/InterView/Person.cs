using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView
{
    public class Person
    {
        public readonly string name;        
        protected int _age;

        public int age{
            get { return this._age; }
            set {
                if(value< this._age)
                {
                    throw new Exception("new age is smaller than orig.");
                }
                else
                {
                    this._age = value;
                }
            } 
        }
        public Person():this("Patrick", 20) { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public override string ToString()
        {
            return string.Format("{0}({1})", name, age) ;
        }
    }
}
