// See https://aka.ms/new-console-template for more information
using college;
using MySqlConnector;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace CollegeEnrollment
{
    class Program
    {
        public static int studentId = 0;
        public static int teacherId = 0;
        public static int adminId = 0;
        public static int superAdminId = 0;       
        public static Student st;
        public static Teacher tchr;
        public static Admin admin;
        public static SuperAdmin sAdmin;

        static void Main(string[] args)
        {   
            Console.WriteLine("** WELCOME TO COLLEGE ENROLLMENT CONSOLE APP **\n");
            Console.Write("    If You are a student Enter:1\n\n    If You are a Teacher Enter:2\n\n" +
                "    If You are a Admin Enter:3\n\n    If You are a SuperAdmin Enter:4\n\nEnter: ");
            int identity = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            
            switch (identity)
            {   
                //STUDENT
                case 1:

                    bool var = true;
                    bool var1 = true;
                    while (var)
                    {
                        Console.WriteLine("\n** WELCOME IN STUDENT SECTION **\n");

                        Console.Write("    If Old Student Enter:1\n\n    If New Student Enter:2\n\nEnter:");
                        int response = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();

                        if (response == 1)
                        {
                            //old Student
                            Console.Write("\nEnter Your StudentId:");
                            studentId = Convert.ToInt32(Console.ReadLine());
                           
                            st = new Student(studentId);//populates all values

                            //verify old student
                            if (st.studentId > 0)
                            {
                                Console.WriteLine("\nStudent Found:)\n");
                                var = false;
                            }
                            else
                            {
                                Console.WriteLine("\nStudent not exist with StudentId = " + studentId);

                                Console.Write("\n   Try again? (yes/no)\n   Enter:");
                                if (Console.ReadLine().ToLower() != "yes")
                                {
                                    Console.WriteLine("\n   << Thank You >>");
                                    var = false;
                                    var1 = false;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        else if (response == 2)
                        {
                            //To Register new Student                         
                            st = new Student();
                            st.RegisterStudent();
                            studentId = Convert.ToInt32(st.studentId);
                            var = false;

                        }
                        else
                        {
                            Console.WriteLine("\n   Enter valid Input(1,2)\n");                           
                        }
                    }
                    
                    while (var1)
                    {
                        Console.WriteLine("\n## STUDENT MENU ##\n");
                        Console.Write("   ~View Student Information Enter:1\n\n   ~View Courses Enrolled Enter:2\n\n" +
                            "   ~Enroll for new Course Enter:3\n\n   ~Update student Information Enter:4\n\n" +
                            "   ~Remove Student Enter:5\n\n   ~ViewResult:6\n\n   ~Exit Enter:7\n\nEnter:");
                        int sReasponse = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");

                        switch (sReasponse)
                        {
                            case 1:
                                st.ViewStudent();//shows student info
                                break;

                            case 2:
                                st.StudentCourseEnrolled();//Show Enrolled Courses
                                break;

                            case 3:
                                CourseEnrollment crs = new CourseEnrollment();
                                crs.EnrollNewCourse(studentId);//Enroll New Course
                                break;

                            case 4:
                                st.UpdateStudent();//Update Student Information
                                break;
                            case 5:
                                st.RemoveStudent();
                                var1 = false;
                                break;

                            case 6:
                                st.ViewResult();
                                break;

                            case 7:
                                var1 = false;//Exit
                                break;

                            default:
                                Console.WriteLine("\n   Please Enter a Valid Input(1 to 7) \n");
                                break;
                        }
                    }
                    
                break;
                
                //TEACHER
                case 2:
                   
                    Console.WriteLine("\n** WELCOME IN TEACHER SECTION **");

                    bool var2 = true;
                    bool var3 = true;
                    while (var2) 
                    { 
                        Console.Write("\n    Are you old Teacher Enter:1\n\n    Add New Teacher Enter:2\n\nEnter:");
                        int input = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                    
                        if(input == 1)
                        {   
                            //Old Teacher
                            Console.Write("\n    Enter Your TeacherId:");
                            teacherId = Convert.ToInt32(Console.ReadLine());

                            tchr = new Teacher(teacherId);                          

                            //Verify Old Teacher
                            if(tchr.teacherId > 0)
                            {     
                                Console.WriteLine("    Teacher exists:)\n");
                                var2 = false;
                            }
                            else
                            {
                                Console.WriteLine("    Teacher not exist with TeacherId = " + teacherId + "\n");
                                Console.Write("\n   Try again ? (yes / no)\n   Enter: ");

                                if (Console.ReadLine().ToLower() != "yes")
                                {
                                    Console.WriteLine("\n    ** Thank You **");
                                    var2 = false;
                                    var3 = false;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        else if(input == 2)
                        {
                            //Register new Teacher                         
                            tchr = new Teacher();
                            tchr.RegisterTeacher();
                            teacherId = Convert.ToInt32(tchr.teacherId);
                            var2 = false;
                        }
                        else
                        {
                            Console.WriteLine("\n   Enter valid Input(1,2)\n");
                        }
                    }

                    
                    while (var3)
                    {
                        Console.WriteLine("\n## TEACHER MENU ##\n");
                        Console.Write("   ~View teacher Information Enter:1\n\n   ~Update teacher Information Enter:2\n\n" +
                            "   ~Course Allocated Enter:3\n\n   ~View Student List Enter:4\n\n   ~Add Result of Your Course Enter:5\n\n" +
                            "   ~Remove Teacher Enter:6\n\n   ~Exit Enter:7\n\nEnter:");
                        int tResponse = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        switch (tResponse) 
                        {
                            case 1:
                                //View Teacher
                                tchr.Viewteacher();
                                break;
                            
                            case 2:
                                //Update Teacher Info                           
                                tchr.UpdateTeacher();
                                break;

                            case 3:
                                //Course Allocated
                                tchr.CourseAllocated();
                                break;

                            case 4:
                                //View Students Taken My course
                                tchr.ListOfStudent();
                                break;
                            case 5:
                                //Add Result
                                tchr.Result();
                                break;

                            case 6:
                                //Remove Teacher
                                tchr.RemoveTeacher();
                                var3 = false;
                                break;

                            case 7:
                                //Exit
                                var3 = false;
                                break;

                            default:
                                Console.WriteLine("\n   Please Enter a Valid Input(1 to 7) \n");
                                break;
                        }
                    }
                    
                break;

                //ADMIN
                case 3:

                    Console.WriteLine("## WELCOME TO ADMIN SECTION ##\n");

                    bool var4 = true;
                    bool var5 = true;
                    while (var4)
                    {                       
                            Console.Write("   Enter Your AdminId:");
                            adminId = Convert.ToInt32(Console.ReadLine());

                            admin = new Admin(adminId);

                            if (admin.adminId > 0)
                            {
                                Console.WriteLine("   Admin exists:)\n");
                                var4 = false;
                            }    
                            else
                            {
                                Console.WriteLine("   Admin not exist with AdminId = " + adminId + "\n\n");
                                Console.Write("Try again? (yes/no)\nEnter:");
                                if (Console.ReadLine().ToLower() != "yes")
                                {
                                    Console.WriteLine("   << THANK YOU >>");
                                    var4 = false;
                                    var5 = false;
                                }
                                else
                                {
                                    continue;
                                }
                            }                      
                    }

                    while (var5)
                    {
                        Console.WriteLine("\n## ADMIN MENU ##\n");
                        Console.Write("   ~View Admin Information Enter:1\n\n   ~For Teacher Section Enter:2\n\n" +
                            "   ~For Student Section Enter:3\n\n   ~View Departments Enter:4\n\n" +
                            "   ~View Courses Enter:5\n\n   ~View Semesters Enter:6\n\n   ~For Exit Enter:7\n\nEnter:");

                        int choise = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");//To clear scrollback console screen
                        switch (choise)
                        {
                            case 1:
                                //View Admin
                                admin.ViewAdmin();
                                break;

                            case 2:
                                //Teacher Section
                                tchr = new Teacher();

                                bool a = true;
                                while (a)
                                {
                                    Console.WriteLine("## TEACHER SECTION ##\n");
                                    Console.Write("   ~View Teachers List Enter:1\n\n   ~View Teacher By TeacherId Enter:2\n\n" +
                                        "   ~Add Teacher Enter:3\n\n   ~Update Teacher Enter:4\n\n" +
                                        "   ~Remove Teacher Enter:5\n\n   ~For Exit Enter:6\n\nEnter:");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (input)
                                    {
                                        case 1:
                                            tchr.ViewTeachersList();
                                            break;

                                        case 2:
                                            Console.Write("   Enter TeacherId:");
                                            teacherId = Convert.ToInt32(Console.ReadLine());
                                            tchr = new Teacher(teacherId);
                                            tchr.Viewteacher();
                                            break;

                                        case 3:
                                            tchr.RegisterTeacher();
                                            teacherId = Convert.ToInt32(tchr.teacherId);
                                            break;

                                        case 4:

                                            bool tv = true;
                                            while (tv)
                                            {
                                                Console.Write("   Enter TeacherId:");
                                                teacherId = Convert.ToInt32(Console.ReadLine());
                                                tchr = new Teacher(teacherId);
                                                if (tchr.teacherId > 0)
                                                {
                                                    tchr.UpdateTeacher();
                                                    tv = false;
                                                }                                      
                                                else Console.WriteLine("\n   You are updating Non-Existing Teacher Enter Again\n");
                                            }
                                            break;

                                        case 5:
                                            Console.Write("   Enter TeacherId:");
                                            teacherId = Convert.ToInt32(Console.ReadLine());
                                            tchr = new Teacher(teacherId);
                                            tchr.RemoveTeacher();
                                            break;

                                        case 6:
                                            a = false;
                                            break;

                                        default:
                                            Console.WriteLine("\n   Please Enter a valid Input(1 to 6)\n");
                                            break;
                                    }
                                }
                                break;

                            case 3:
                                //Student Section
                                st = new Student();

                                bool b = true;
                                while (b)
                                {
                                    Console.WriteLine("## STUDENT SECTION ##\n");
                                    Console.Write("   ~View Student List Enter:1\n\n   ~View Student By StudentId Enter:2\n\n" +
                                        "   ~Add Student Enter:3\n\n   ~Update Student Enter:4\n\n" +
                                        "   ~Remove Student Enter:5\n\n   ~For Exit Enter:6\n\nEnter:");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (input)
                                    {
                                        case 1:
                                            st.ViewStudentsList();
                                            break;

                                        case 2:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.ViewStudent();
                                            break;

                                        case 3:
                                            st.RegisterStudent();
                                            studentId = Convert.ToInt32(st.studentId);
                                            break;

                                        case 4:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.UpdateStudent();
                                            break;

                                        case 5:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.RemoveStudent();
                                            break;

                                        case 6:
                                            b = false;
                                            break;

                                        default:
                                            Console.WriteLine("\n   Please Enter a valid Input(1 to 6)\n");
                                            break;
                                    }
                                }
                                break;

                            case 4:
                                //View Departments
                                Department dept = new Department();
                                dept.ViewDepartmentList();
                                break;

                            case 5:
                                //View Courses
                                Course crs = new Course();
                                crs.ViewCoursesList();
                                break;
                            case 6:
                                //View Semesters
                                Semester sem = new Semester();
                                sem.ViewSemesters();
                                break;
                            case 7:
                                //Exit
                                var5 = false;
                                break;
                            default:
                                Console.WriteLine("\n   Invalid Input (Enter 1 to 7 only)\n");
                                break;
                        }
                    }
                    break;

                //SuperAdmin 
                case 4:

                    Console.WriteLine("\n## WELCOME TO SUPER-ADMIN SECTION ##");

                    bool var6 = true;
                    bool var7 = true;

                    while (var6)
                    {                       
                        Console.Write("\n   Enter Your SuperAdminId:");
                        superAdminId = Convert.ToInt32(Console.ReadLine());

                        sAdmin = new SuperAdmin(superAdminId);

                        if (sAdmin.superAdminId > 0)
                        {
                            Console.WriteLine("   SuperAdmin exists:)");
                            var6 = false;
                        }
                        else
                        {
                            Console.WriteLine("   SuperAdmin not exist with Id = " + superAdminId + "\n\n");
                            Console.Write("Try again? (yes/no)\nEnter:");
                            if (Console.ReadLine().ToLower() != "yes")
                            {
                                Console.WriteLine("\n   << THANK YOU >>");
                                var6 = false;
                                var7 = false;
                            }
                            else
                            {
                                Console.Clear();
                                continue;
                            }
                        }
                    }

                    while (var7)
                    {
                        Console.WriteLine("\n## SUPER-ADMIN MENU ##");
                        Console.Write("\n   ~View SuperAdmin Information Enter:1\n\n   ~Department Section Enter:2\n\n" +
                            "   ~Course Section Enter:3\n\n   ~Semester Section Enter:4\n\n   ~Admin Section Enter:5\n\n" +
                            "   ~Teacher Section Enter:6\n\n   ~Student Section Enter:7\n\n" +
                            "   ~Update SuperAdmin Information Enter:8\n\n   ~To Exit Supr-Admin Section Enter:9\n\nEnter:");//Result section remaining
                        int choise = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\x1b[3J");
                        switch (choise)
                        {
                            case 1:
                                //View SuperAdmin Info
                                sAdmin.ViewSuperAdmin();
                                break;

                            case 2:
                                //Department Section
                                
                                Department department = new Department();
                                int deptId;
                                bool var8 = true;
                                while (var8)
                                {
                                    Console.WriteLine("\n## DEPARTMENT SECTION ##\n");
                                    Console.Write("   ~Department List Enter:1 \n\n   ~View Department by Id Enter:2 \n\n" +
                                        "   ~Add Department Enter:3 \n\n   ~Update Department Enter:4 \n\n" +
                                        "   ~Remove Department Enter:5\n\n   ~To Exit Department Section Enter:6\n\nEnter:");
                                    int response = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");

                                    switch (response)
                                    {
                                        case 1:
                                            department.ViewDepartmentList();
                                            break;

                                        case 2:
                                            Console.Write("\n   Enter DeptId:");
                                            deptId = Convert.ToInt32(Console.ReadLine());
                                            department = new Department(deptId);
                                            department.ViewDept();
                                            break;

                                        case 3:
                                            department.AddNewDepartment();
                                            break; 

                                        case 4:                                            
                                            Console.Write("\n    Enter DeptId:");
                                            deptId = Convert.ToInt32(Console.ReadLine());
                                            department = new Department(deptId);
                                            department.UpdateDepartment();
                                            break;
                                            
                                        case 5:
                                            Console.Write("\n    Enter DeptId:");
                                            deptId = Convert.ToInt32(Console.ReadLine());
                                            department = new Department(deptId);
                                            department.RemoveDept();
                                            break;  
                                            
                                        case 6:
                                            var8 = false;
                                            break; 
                                        
                                        default: 
                                            Console.WriteLine("\n   Enter Correct Digit (1 to 6)\n"); 
                                            break;
                                    }
                                }
                                break;

                            case 3:
                                //Course Section
                                
                                Course crs1 = new Course();
                                bool var9 = true;
                                while (var9)
                                {
                                    Console.WriteLine("\n## COURSE SECTION ##\n");
                                    Console.Write("   ~Courses List Enter:1 \n\n   ~View Course by Id Enter:2\n\n" +
                                    "   ~Add Course Enter:3 \n\n   ~Update Course Enter:4 \n\n" +
                                    "   ~Remove Course Enter:5\n\n   ~To Exit Course Section Enter:6\n\nEnter:");
                                    int response = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");                                   
                                    switch (response)
                                    {
                                        case 1:                                          
                                            crs1.ViewCoursesList();

                                            break;

                                        case 2:
                                            Console.Write("\n    Enter CourseId:");
                                            int CrsId = Convert.ToInt32(Console.ReadLine());
                                            crs1 = new Course(CrsId);
                                            crs1.ViewCourse();

                                            break;

                                        case 3:                                        
                                            crs1.AddNewCourse();

                                            break;

                                        case 4:
                                            crs1.UpdateCourse();

                                            break;

                                        case 5:
                                            Console.Write("\n   To Remove Course Enter CourseId:");
                                            int cId = Convert.ToInt32(Console.ReadLine());
                                            crs1 = new Course(cId);
                                            crs1.RemoveCourse();

                                            break;

                                        case 6:
                                            var9 = false;

                                            break;

                                        default:
                                            Console.WriteLine("\n   Enter Correct Digit (1 to 6)\n");

                                            break;
                                    }
                                }
                                break;

                            case 4:
                                //Semester Section
                                
                                Semester sem = new Semester();
                                bool varr = true;
                                while (varr)
                                {
                                    Console.WriteLine("\n## SEMESTER SECTION ##\n");
                                    Console.Write("   ~Semester List Enter:1 \n\n   ~View Semester by Id Enter:2\n\n" +
                                        "   ~Add Semester Enter:3 \n\n   ~Update Semester Enter:4 \n\n   ~Remove Semester Enter:5\n\n" +
                                    "   ~To Exit Semester Section Enter:6\n\nEnter: ");
                                    int response = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (response)
                                    {
                                        case 1:
                                            sem.ViewSemesters();

                                            break;

                                        case 2:
                                            sem.ViewSemester();

                                            break;

                                        case 3:                                            
                                            sem.AddNewSemester();

                                            break;

                                        case 4:
                                            Console.Write("   To Update Enter SemId:");
                                            int semId = Convert.ToInt32(Console.ReadLine());
                                            sem = new Semester(semId);
                                            sem.UpdateSemester();

                                            break;

                                        case 5:
                                            Console.Write("\n   Enter SemesterId to Remove:");
                                            int sId = Convert.ToInt32(Console.ReadLine());
                                            sem = new Semester(sId);
                                            sem.RemoveSemester();

                                            break;

                                        case 6:
                                            varr = false;

                                            break;

                                        default:
                                            Console.WriteLine("\n    Enter Correct Digit (1 to 6)\n");

                                            break;
                                    }
                                }
                                break;

                            case 5:
                                //Admin Section
                              
                                admin = new Admin();

                                bool varr2 = true;
                                while (varr2)
                                {
                                    Console.WriteLine("## ADMIN SECTION ##\n");
                                    Console.Write("   ~View Admins List Enter:1\n\n   ~View Admin By AdminId Enter:2\n\n" +
                                        "   ~Add new Admin Enter:3\n\n   ~Update Admin Enter:4\n\n" +
                                        "   ~Remove Admin Enter:5\n\n   ~To Exit Admin Section Enter:6\n\nEnter:");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (input)
                                    {
                                        case 1:
                                            admin.ViewAdminsList();

                                            break;

                                        case 2:
                                            Console.Write("\n   Enter AdminId:");
                                            adminId = Convert.ToInt32(Console.ReadLine());
                                            admin = new Admin(adminId);
                                            admin.ViewAdmin();
                                            
                                            break;

                                        case 3:
                                            admin.RegisterAdmin();

                                            break;

                                        case 4:
                                            Console.Write("\n   Enter AdminId to Update:");
                                            adminId = Convert.ToInt32(Console.ReadLine());
                                            admin = new Admin(adminId);
                                            admin.UpdateAdmin();
                                            
                                            break;

                                        case 5:
                                            Console.Write("\n   Enter AdminId to Remove:");
                                            adminId = Convert.ToInt32(Console.ReadLine());
                                            admin = new Admin(adminId);
                                            admin.RemoveAdmin();

                                            break;

                                        case 6:
                                            varr2 = false;

                                            break;

                                        default:
                                            Console.WriteLine("\n   Please Enter a valid Input(1 to 6)\n");

                                            break;
                                    }
                                }
                                break;

                            case 6:
                                //Teacher Section
                                
                                tchr = new Teacher();

                                bool varr3 = true;
                                while (varr3)
                                {
                                    Console.WriteLine("\n## TEACHER SECTION ##\n");
                                    Console.Write("   ~View Teachers List Enter:1\n\n   ~View Teacher By TeacherId Enter:2\n\n" +
                                        "   ~Add Teacher Enter:3\n\n   ~Update Teacher Enter:4\n\n" +
                                        "   ~Remove Teacher Enter:5\n\n   ~For Exit Teacher Section Enter:6\n\nEnter:");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (input)
                                    {
                                        case 1:
                                            tchr.ViewTeachersList();

                                            break;

                                        case 2:
                                            Console.Write("   Enter TeacherId:");
                                            teacherId = Convert.ToInt32(Console.ReadLine());
                                            tchr = new Teacher(teacherId);                                            
                                            tchr.Viewteacher();
                                            
                                            break;

                                        case 3:
                                            tchr.RegisterTeacher();
                                            teacherId = Convert.ToInt32(tchr.teacherId);

                                            break;

                                        case 4:
                                            Console.Write("   Enter TeacherId:");
                                            teacherId = Convert.ToInt32(Console.ReadLine());
                                            tchr = new Teacher(teacherId);
                                            tchr.UpdateTeacher();

                                            break;

                                        case 5:
                                            Console.Write("   Enter TeacherId:");
                                            teacherId = Convert.ToInt32(Console.ReadLine());
                                            tchr = new Teacher(teacherId);
                                            tchr.RemoveTeacher();

                                            break;

                                        case 6:
                                            varr3 = false;
                                            break;

                                        default:
                                            Console.WriteLine("\n   Please Enter a valid Input(1 to 6)\n");

                                            break;
                                    }
                                }
                                break;

                            case 7:
                                //Student Section
                                
                                st = new Student();

                                bool varr1 = true;
                                while (varr1)
                                {
                                    Console.WriteLine("## STUDENT SECTION ##\n");
                                    Console.Write("   ~View Student List Enter:1\n\n   ~View Student By StudentId Enter:2\n\n" +
                                        "   ~Add Student Enter:3\n\n   ~Update Student Enter:4\n\n" +
                                        "   ~Remove Student Enter:5\n\n   ~To Exit Student Section Enter:6\n\nEnter:");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Console.WriteLine("\x1b[3J");
                                    switch (input)
                                    {
                                        case 1:
                                            st.ViewStudentsList();

                                            break;

                                        case 2:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.ViewStudent();

                                            break;

                                        case 3:
                                            st.RegisterStudent();
                                            studentId = Convert.ToInt32(st.studentId);

                                            break;

                                        case 4:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.UpdateStudent();

                                            break;

                                        case 5:
                                            Console.Write("   Enter StudentId:");
                                            studentId = Convert.ToInt32(Console.ReadLine());
                                            st = new Student(studentId);
                                            st.RemoveStudent();

                                            break;

                                        case 6:
                                            varr1 = false;

                                            break;

                                        default:
                                            Console.WriteLine("\n   Please Enter a valid Input(1 to 6)\n");

                                            break;
                                    }
                                }
                                break;

                            case 8:
                                //Update SuperAdmin Information
                                sAdmin.UpdateSuperAdmin();

                                break;

                            case 9:
                                //Exit
                                var7 = false;

                                break;

                            default:
                                Console.WriteLine("\n   Invalid Input (Enter 1 to 8 only)\n");
                                break;
                        } 
                    }
                    break;

                default:
                    Console.WriteLine("\n   Invalid Input (Student Enter 1,Teacher Enter:2,Admin Enter:3, SuperAdmin Enter:4)\n");
                    break;
            }

        }//end of Main() class
    }
}