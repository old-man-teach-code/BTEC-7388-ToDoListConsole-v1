using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_se08203
{
    class Program
    {
        static string[] todolist = new string[1000]; // Danh sách công việc (tối đa 1000 công việc)
        static int n_tasks = 0; // Số lượng công việc trong todolist
        static bool[] todolist_status = new bool[1000]; // Trạng thái của công việc (true: đã hoàn thành, false: chưa hoàn thành)
        static double[] todolist_hour = new double[1000]; // Thời gian ước tính cho mỗi công việc (theo giờ)
        static int[] todolist_type = new int[1000]; // Loại công việc [WebApp / AI engineer / House cleaning]
        static int chooseTaskType() // Validate việc nhập lựa chọn đúng
        {
            bool isCorrectChoice = false;
            int choice = 0;
            while (!isCorrectChoice)
            {
                try
                {
                    Console.WriteLine("Choose the task type: ");
                    Console.WriteLine("1 - Web/App");
                    Console.WriteLine("2 - AI Engineer");
                    Console.WriteLine("3 - House Cleaning");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice != 1 && choice != 2 && choice != 3)
                    {
                        isCorrectChoice = false;
                    }
                    else
                    {
                        isCorrectChoice = true;
                    }
                }
                catch (Exception ex)
                {
                    isCorrectChoice= false;
                }
               
            }
            return choice;
        }

        static string getTaskTypeName(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return "WebApp";
                case 2:
                    return "AI Engineer";
                case 3:
                    return "House Cleaning";
                default:
                    return "Undefined task";
            }
        }

        static double getTaskCost(int typeId, double taskHour) // Tính giá task theo loại task
        {
            switch (typeId)
            {
                case 1:
                    return taskHour * 210000;
                case 2:
                    return taskHour * 1000000;
                case 3:
                    return taskHour * 1100000;
                default:
                    return -1;
            }
        }

        static void printToDoList()
        {
            Console.WriteLine("----------------Current To do list------------------");
            for (int i = 0; i < n_tasks; i++)
            {
                Console.WriteLine("Task no{0}: {1} - estimate {2} - Type: {3} - state: {4} --> Cost: {5}", i + 1, todolist[i], todolist_hour[i], 
                    getTaskTypeName(todolist_type[i]),
                    todolist_status[i] ? " Done" : " Not done",
                    getTaskCost(todolist_type[i], todolist_hour[i])
                    );
                // Toán tử 3 ngôi cấu trúc:
                // [Ngôi 1 - giá trị true/false] ? [Ngôi 2 - Giá trị nếu ngôi 1 đúng] : [Ngôi 3 - Giá trị nếu ngôi 2 sai]
            }
        }

        static int inputChoice() // Validate lựa chọn
        {
            int choice = 0;
            bool isCorrectChoice = false;
            while (!isCorrectChoice)
            {
                try
                {
                    Console.Write("Would you like to continue? (1: Yes, 0: No): ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice != 0 && choice != 1) 
                    {
                        isCorrectChoice = false; // Nếu không phải 0 hoặc 1 thì yêu cầu nhập lại
                    }
                    else
                    {
                        isCorrectChoice = true; // Nếu là 0 hoặc 1 thì thoát khỏi vòng lặp
                    }

                }
                catch (Exception e)
                {
                    isCorrectChoice = false; // Nếu có lỗi xảy ra trong quá trình chạy thì cho nhập lại choice
                }
            }

            return choice;
        }

        static void createToDoList()
        {
            int choice = 1; // trạng thái ban đầu  = 1 -> nhập 
            while (choice == 1)
            {
                try
                {
                    // TODO: Tăng biến n_tasks lên 1 đơn vị (vừa nhập thêm 1 task)
                    n_tasks++;
                    // TODO: Nhập công việc vào todolist
                    Console.Write("Input the task no{0}: ", n_tasks);
                    todolist[n_tasks - 1] = Console.ReadLine();
                    // TODO: Nhập time estimate công việc vào todolist_hour
                    Console.Write("Input estimate hour of the task no{0}: ", n_tasks);
                    todolist_hour[n_tasks - 1] = Convert.ToDouble(Console.ReadLine());
                    // TODO: Nhập loại công việc
                    todolist_type[n_tasks - 1] = chooseTaskType();
                    // TODO: Nhập trạng thái của công việc (hoàn thành hay chưa) vào todolist_status (mặc định là chưa)
                    todolist_status[n_tasks - 1] = false;
                    // TODO: Hỏi người dùng có muốn nhập tiếp hay không và đổi trạng thái của choice

                    choice = inputChoice();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Existed Error: " + ex.Message);
                    choice = 0;
                }
            }
        }

        static void updateAllTaskStatus()
        {
            Console.WriteLine("----------------Update To do list------------------");
            for (int i = 0; i < n_tasks; i++)
            {
                Console.WriteLine("Have you finish the no{0} task {1}: ", i + 1, todolist[i]);
                Console.Write("Input your answer (1: Yes, 0: No): ");
                int answer = Convert.ToInt32(Console.ReadLine());
                if (answer == 1)
                {
                    todolist_status[i] = true; // Đánh dấu công việc đã hoàn thành
                }
                else
                {
                    todolist_status[i] = false; // Không cần thiết, vì mặc định là false
                }
            }
        }

        static void menu()
        {
            int choice = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Choose your action: ");
                    Console.WriteLine("0. End the program");
                    Console.WriteLine("1. Input task");
                    Console.WriteLine("2. Read tasks");
                    Console.WriteLine("3. Update all tasks status");
                    Console.WriteLine("4. Remove a task");
                    Console.WriteLine("5. Update task content");
                    Console.WriteLine("Your action is: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Good bye");
                            return;
                        case 1:
                            Console.WriteLine("Input task");
                            createToDoList();
                            break;
                        case 2:
                            Console.WriteLine("Read tasks");
                            printToDoList();
                            break;
                        case 3:
                            Console.WriteLine("update all tasks");
                            updateAllTaskStatus();
                            break;
                        case 4:
                            Console.WriteLine("remove a task");
                            removeTask();
                            break;
                        case 5:
                            updateTaskContent();
                            break;
                        case 6:
                            findTaskbyPrice();
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            break;
                    }
                    Console.WriteLine("-----------------------------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " +ex.Message);
                }
            }
        }

        static void updateTaskContent()
        {
            //TODO: code update nội dung 1 task được chọn
        }
        static void findTaskbyPrice()
        {
            //TODO: Tìm kiếm task theo giá tiền nhập vào
        }

        static void removeTask()
        {      
            if (n_tasks >= 1)
            {
                Console.WriteLine("Input the task number which you wanna remove:");
                int choosenIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                for (int i = choosenIndex; i < (n_tasks - 1); i++)
                {
                    todolist[i] = todolist[i + 1];
                    todolist_status[i] = todolist_status[i + 1];
                    todolist_hour[i] = todolist_hour[i + 1];
                    todolist_type[i] = todolist_type[i + 1];
                }

                todolist[n_tasks - 1] = null;
                todolist_status[n_tasks - 1] = new bool();
                todolist_hour[n_tasks - 1] = new int();
                todolist_type[n_tasks - 1] = new int();
                n_tasks--;
            }
            else
            {
                Console.WriteLine("There is no task");
            }

        }

        static void Main(string[] args)
        {
            menu();
        }
    }
}
