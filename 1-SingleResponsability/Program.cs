using SingleResponsability;

StudentRepository studentRepository = new();
studentRepository.GetAll().Export();
Console.WriteLine("Proceso Completado");