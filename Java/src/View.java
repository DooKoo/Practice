import javax.swing.*;
import javax.swing.table.AbstractTableModel;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
/**
 * Created by DooKoo on 17.06.2014.
 */
public class View extends JFrame {
    private MCollection<Student> MainData= new MCollection<Student>();

    // Buttons.
    private JButton AddButton;
    private JButton DeleteButton;
    private JButton ClearButton;
    private JButton AZButton;
    private JButton ZAButton;
    private JButton FindButton;

    // Fields for input data.
    private JTextField InputName= new JTextField(10);
    private JComboBox<Integer> CourseBox= new JComboBox<Integer>(new Integer[]{1,2,3,4,5});
    private JCheckBox CheckArmy= new JCheckBox("");

    // Labels.
    private JLabel NameLabel = new JLabel("Name:");
    private JLabel CourseLabel = new JLabel("Course:");
    private JLabel ArmyLabel = new JLabel("Army:");

    // Layouts.
    private JFrame MainFrame = new JFrame("Window");
    private JPanel ButtonPanel = new JPanel(new FlowLayout());
    private JPanel FieldPanel = new JPanel(new FlowLayout());
    private JPanel TablePanel = new JPanel(new FlowLayout());

    // Table for view data from the collection.
    private TableModel MainTableModel = new TableModel(MainData);
    private JTable Table = new JTable(MainTableModel);
    private JScrollPane TableScrollPane = new JScrollPane(Table);

    public View()
    {
        MainFrame.setLayout(new FlowLayout());
        MainFrame.setSize(600,600);
        MainFrame.setVisible(true);
        MainFrame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        JButton[] ArrayOfButton= new JButton[6];

        ArrayOfButton[0]=AddButton =new JButton("Add");
        ArrayOfButton[1]=DeleteButton = new JButton("Delete");
        ArrayOfButton[2]=ClearButton = new JButton("Clear");
        ArrayOfButton[3]=AZButton = new JButton("A..Z sort");
        ArrayOfButton[4]=ZAButton = new JButton("Z..A Sort");
        ArrayOfButton[5]=FindButton = new JButton("Find");

        // Adds Buttons to ButtonPanel.
        for(JButton item :ArrayOfButton)
        {
            ButtonPanel.add(item);
        }

        // Temporary for test.
        Student tmp= new Student();
        tmp.Name = "Lol";
        tmp.Course = 4;
        tmp.Army = true;
      //  MainData.AddToEnd(tmp);

        // Adds elements to FieldPanel.
        FieldPanel.add(NameLabel);
        FieldPanel.add(InputName);
        FieldPanel.add(CourseLabel);
        FieldPanel.add(CourseBox);
        FieldPanel.add(ArmyLabel);
        FieldPanel.add(CheckArmy);

        // Adds element to TablePanel.
        TablePanel.add(TableScrollPane);

        // Adds layouts to MainFrame.
        MainFrame.add(FieldPanel);
        MainFrame.add(ButtonPanel);
        MainFrame.add(TablePanel);

        // Actions for Buttons.
        AddButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                Student std = new Student(InputName.getText(), CourseBox.getSelectedIndex()+1, CheckArmy.isSelected());
                MainData.AddToEnd(std);
                Table.updateUI();
            }
        });
        ClearButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MainData.Clear();
                Table.updateUI();
            }
        });
        AZButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MainData.SortMin();
                Table.updateUI();
            }
        });
        ZAButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MainData.SortMax();іфв
                Table.updateUI();
            }
        });
        DeleteButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MainData.Delete(Table.getSelectedRow());
                Table.updateUI();
            }
        });
        FindButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MainData = MainData.Find(new Student(InputName.getText(), CourseBox.getSelectedIndex()+1, CheckArmy.isSelected()));
                Table.updateUI();
            }
        });
    }
    private  class TableModel extends AbstractTableModel {

        public TableModel(MCollection<Student> data)
        {
               MainData = data;
        }

        @Override
        public int getRowCount() {
            return MainData.getCount();
        }

        @Override
        public int getColumnCount() {
            return 3;
        }

        @Override
        public Object getValueAt(int rowIndex, int columnIndex) {
            switch(columnIndex) {
                case 0:
                    return MainData.GetElement(rowIndex).Name;
                case 1:
                    return MainData.GetElement(rowIndex).Course;
                case 2:
                    return MainData.GetElement(rowIndex).Army;
                default:
                    return null;
            }
        }
    }

}

