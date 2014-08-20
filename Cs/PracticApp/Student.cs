using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticApp
{
    public class Student:IComparable
    {
       public enum Names
        {
           Dexter,
           Joseph,
           Paul,
           None, 
           Thomas,
           Vincent
        };
        public Names Name{get; set;}
        public int Course { get; set; }
        public bool Army { get; set; }
        static public Names ConvertToNames(string _input)
        {
            switch (_input)
            {
                case "Paul":
                    return Names.Paul;
                case "Dexter":
                    return Names.Dexter;
                case "Vincent":
                    return Names.Vincent;
                case "Thomas":
                    return Names.Thomas;
                case "Joseph":
                    return Names.Joseph;
                default:
                    return Names.None;

            }
        }
        public override bool Equals(object obj)
        {
            Student val = (Student)obj;
            if ((val.Name == Name) && (val.Course == Course) && (val.Army == Army))
                return true;
            else
                return false;
        }
        public int CompareTo(object obj)
        {
            Student val = (Student)obj;
            if (Name > val.Name)
                return 666;
            else if (Name < val.Name)
                return -666;
            else
            {
                if (Course > val.Course)
                    return 666;
                else if (Course < val.Course)
                    return -666;
                else
                {
                    if (Convert.ToInt32(Army) > Convert.ToInt32(val.Army))
                        return 666;
                    else if (Convert.ToInt32(Army) < Convert.ToInt32(val.Army))
                        return -666;
                    else
                        return 0;
                }
                
            }
            
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
