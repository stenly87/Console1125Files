using System.IO;
using System.Text;
namespace Console1125Files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // работа с файлами в C#
            // System.IO
            // 2 вида путей: абсолютные и относительные
            // абсолютный путь: C:\Users\Student\source\repos\Console1125Files\Console1125Files
            // относительный путь: включает по умолчанию рабочую папку приложения и добавляет к ней указанный путь
            // если используется относительный путь, то работает
            // правило .. - переход к директории на уровень выше (предыдущей директории)

            //Directory.Delete("Images", true); // удаление директории (второй аргумент это рекурсивное удаление для удаления содержимого)

            // перехват исключений:
            try
            {
                //Directory.CreateDirectory("Imag:?es/Second/Path"); //- создание директории
                //Console.WriteLine("мы не увидим этот текст");
                // перемещение директории
                //Directory.Move("путь до перемещаемой директории",
                //    "путь, куда мы все перемещаем");


                //Directory.Move(@"C:\Users\Student\source\repos\Console1125Files\Console1125Files\bin\Debug\net8.0\Images",
                //    "C:\\Users\\Student\\source\\repos\\Console1125Files\\Console1125Files\\bin\\Debug\\net8.0\\target");

                // получить список файлов из указанной директории
                // можно указать шаблон для фильтрации файлов по имени
                // можно указать опции, чтобы файлы выводились из поддиректорий
                // результат - массив абсолютных путей к файлам
                // получить рабочую папку:
                /*Console.WriteLine(Environment.CurrentDirectory);
                
                string[] files = Directory.GetFiles(@"C:\Users\Student\source\repos\Console1125Files", "*.cs", SearchOption.AllDirectories);
                foreach (var file in files)
                    Console.WriteLine(file);
                */

                string str1 = Environment.CurrentDirectory + "\\Images";
                Console.WriteLine(str1);
                // можно использовать Path.Combine для построения пути
                str1 = Path.Combine(Environment.CurrentDirectory, "target");
                Console.WriteLine(str1);

                // такой же вывод директорий, как с файлами
                string[] dirs = Directory.GetDirectories(str1);

                //для работы с конкретной директорией можно использовать
                // класс DirectoryInfo

                DirectoryInfo directoryInfo = new DirectoryInfo(str1);
                string name = directoryInfo.Name; // получить имя директории
                Console.WriteLine($"{name}");
                bool check = directoryInfo.Exists; // проверка на существование директории

                // сюда пишем код, который потенциально 
                // может вызвать исключения
            }
            catch (Exception ex) // можно добавить блок с переменной, куда будет сохраняться инфрормация об исключении
            {
                Console.WriteLine(ex.HResult);
                Console.WriteLine(ex.Message);
                // выполняется в случае перехвата исключения
            }
            finally // необязательная часть
            {
                // этот блок выполнится в любом случае
            }

            // работа с файлами
            // работа с файлами - 2 варианта
            // чтение и запись файла целиком
            // чтение и запись с помощью потока (стрима)

            // создание файлов целиком
            File.WriteAllText("file1.txt", "test"); // utf8
            File.WriteAllLines("file2.txt", ["текст1", "текст2"]);// utf8
            File.WriteAllBytes("file3.txt", [156, 157, 158, 159]);// ansi
            // чтение файлов целиком
            byte[] bytes = File.ReadAllBytes("file3.txt");
            string text = File.ReadAllText("file1.txt");
            string[] lines = File.ReadAllLines("file2.txt");

            // дозапись файла (запись в конец файла)
            File.AppendAllLines("file1.txt", ["текст1", "текст2"]);
            File.AppendAllText("file2.txt", "append text");

            // полезная инфа о файле через класс FileInfo
            FileInfo fileInfo = new FileInfo("file1.txt");
            long length = fileInfo.Length; // длина файла в байтах
            string name1 = fileInfo.Name;// имя файла вместе с расширением
            string ext = fileInfo.Extension;// расширение (вместе с точкой)
            Console.WriteLine(ext);
            //fileInfo.MoveTo("новый путь и новое имя"); // перемещение файла
            //fileInfo.Replace("путь к файлу, который заменяем", "бэкап файла");// замена файла с созданием запасной версии

            // Перемещение файла также доступно из класса File
            // File.Move("что", "куда");



            // 2 вариант: стримы 
            // любой стрим работает в режиме курсора: мы находимся на некоторой позиции
            // и относительно этой позиции мы можем писать или читать байты
            // открытие стрима:

            FileStream fs = File.Open("file1.txt", FileMode.Open, FileAccess.Read);
            byte[] bytes1 = new byte[10];
            // я хочу прочить в массив bytes1 первые 10 байт
            // из файла. bytes1.Length - указывает длину (кол-во байт, которые мы будем читать)
            // 0 это сдвиг относительно текущего положения
            fs.Read(bytes1, 0, bytes1.Length);

            fs.Close(); // освобождение файла

            // получение байт из строки
            byte[] str = Encoding.UTF8.GetBytes(text);
            // обратное преобразование байтов в строку
            string text1 = Encoding.UTF8.GetString(str);

            // получение байтов из чисел (символов и bool)
            byte[] byteLong = BitConverter.GetBytes(length);
            // обратное преобразование байтов в число
            long l = BitConverter.ToInt64(bytes1, 0);


            using (FileStream fs1 = File.Open("file1.txt", FileMode.Open, FileAccess.Read))
            {               
                // блок работы с файлом
            }// окончание using автоматически закроет стрим и освободит файл

            using (var fs1 = File.OpenWrite("file2.txt"))
            using (var bw = new BinaryWriter(fs1))
            {
                bw.Write(1);
                bw.Write(1f);
                bw.Write(1d);
                bw.Write(true);
                bw.Write("привет");
                bw.Write('ы');
            }

            // тоже открытие файла на чтение
            using (var fs1 = File.OpenRead("file2.txt"))
            using (var br = new BinaryReader(fs1)) 
            {
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadSingle());
                Console.WriteLine(br.ReadDouble());
                Console.WriteLine(br.ReadBoolean());
                Console.WriteLine(br.ReadString());
                Console.WriteLine(br.ReadChar());
            }

        }
    }
}
