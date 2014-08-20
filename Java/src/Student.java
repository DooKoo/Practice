import com.sun.xml.internal.ws.commons.xmlutil.Converter;

import javax.swing.table.AbstractTableModel;
import java.util.Comparator;

/**
 * Created by DooKoo on 17.06.2014.
 */
public class Student implements Comparable {
    public String Name;
    public int Course;
    public boolean Army;

    public Student()
    {

    }
    public Student(String _name, int _course, boolean _army)
    {
        Name = _name;
        Course = _course;
        Army = _army;
    }

    @Override
    public boolean equals(Object obj) {
        Student val= (Student)obj;
        if(val.Name.equals(Name) && val.Army==Army && val.Course==Course)
            return true;
        else
            return false;

    }


    @Override
    public int compareTo(Object o) {
        Student compare= (Student)o;
        if (Name.compareTo(compare.Name)>0)
            return 666;
        else if (Name.compareTo(compare.Name)<0)
            return -666;
        else
        {
            if(Course>compare.Course)
                return 666;
            else if (Course<compare.Course)
                return -666;
            else
            {
                if((Army?1:0)>(compare.Army?1:0))
                    return 666;
                else if ((Army?1:0)<(compare.Army?1:0))
                    return -666;
                else
                    return 0;
            }

        }

    }
}
