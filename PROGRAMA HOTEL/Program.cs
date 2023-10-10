using System;
using System.Collections.Generic;

class Habitacion
{
    public string Id { get; set; }
    public string Refrigeracion { get; set; }
    public int CantidadCamas { get; set; }
    public decimal Precio { get; set; }
    public bool Reservada { get; set; }
    public int DiasReservada { get; set; }
    public decimal ValorTotal { get; set; }

    public override string ToString()
    {
        string estado = Reservada ? $"Reservada por {DiasReservada} días - Total: ${ValorTotal}" : "Disponible";
        return $"Habitación No {Id} - {Refrigeracion} - Camas {CantidadCamas} - ${Precio} - Estado: {estado}";
    }
}

class Program
{
    static List<Habitacion> habitaciones = new List<Habitacion>();
    static int idCounter = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Área administrador");
            Console.WriteLine("2. Realizar reserva");
            Console.WriteLine("3. Salir");
            Console.Write("Elija una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AreaAdministrador();
                    break;
                case "2":
                    RealizarReserva();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    break;
            }
        }
    }

    static void AreaAdministrador()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Área Administrador:");
            Console.WriteLine("1. Registrar habitación");
            Console.WriteLine("2. Ver habitaciones");
            Console.WriteLine("3. Editar habitación");
            Console.WriteLine("4. Volver");
            Console.Write("Elija una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarHabitacion();
                    break;
                case "2":
                    VerHabitaciones();
                    break;
                case "3":
                    EditarHabitacion();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    break;
            }
        }
    }

    static void RegistrarHabitacion()
    {
        Console.Clear();
        Console.Write("Ingrese la refrigeración (Ventilador/Aire acondicionado): ");
        string refrigeracion = Console.ReadLine();

        Console.Write("Ingrese la cantidad de camas: ");
        int cantidadCamas;
        while (!int.TryParse(Console.ReadLine(), out cantidadCamas) || cantidadCamas <= 0)
        {
            Console.WriteLine("Cantidad de camas debe ser un número entero positivo. Intente de nuevo.");
            Console.Write("Ingrese la cantidad de camas: ");
        }

        Console.Write("Ingrese el precio: $");
        decimal precio;
        while (!decimal.TryParse(Console.ReadLine(), out precio) || precio <= 0)
        {
            Console.WriteLine("Precio debe ser un número positivo. Intente de nuevo.");
            Console.Write("Ingrese el precio: $");
        }

        Habitacion habitacion = new Habitacion
        {
            Id = idCounter.ToString("D3"),
            Refrigeracion = refrigeracion,
            CantidadCamas = cantidadCamas,
            Precio = precio,
            Reservada = false,
            DiasReservada = 0,
            ValorTotal = 0
        };

        habitaciones.Add(habitacion);
        idCounter++;

        Console.WriteLine("Habitación registrada exitosamente.");
        Console.ReadLine();
    }

    static void VerHabitaciones()
    {
        Console.Clear();

        if (habitaciones.Count == 0)
        {
            Console.WriteLine("No hay habitaciones registradas.");
            Console.ReadLine();
            return;
        }

        foreach (var habitacion in habitaciones)
        {
            Console.WriteLine(habitacion.ToString());
        }

        Console.ReadLine();
    }

    static void EditarHabitacion()
    {
        Console.Clear();
        Console.Write("Ingrese el ID de la habitación que desea editar: ");
        string id = Console.ReadLine();

        Habitacion habitacion = habitaciones.Find(h => h.Id == id);

        if (habitacion == null)
        {
            Console.WriteLine("No se encontró una habitación con ese ID.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese la nueva refrigeración (Ventilador/Aire acondicionado): ");
        string refrigeracion = Console.ReadLine();

        Console.Write("Ingrese la nueva cantidad de camas: ");
        int cantidadCamas;
        while (!int.TryParse(Console.ReadLine(), out cantidadCamas) || cantidadCamas <= 0)
        {
            Console.WriteLine("Cantidad de camas debe ser un número entero positivo. Intente de nuevo.");
            Console.Write("Ingrese la nueva cantidad de camas: ");
        }

        Console.Write("Ingrese el nuevo precio: $");
        decimal precio;
        while (!decimal.TryParse(Console.ReadLine(), out precio) || precio <= 0)
        {
            Console.WriteLine("Precio debe ser un número positivo. Intente de nuevo.");
            Console.Write("Ingrese el nuevo precio: $");
        }

        habitacion.Refrigeracion = refrigeracion;
        habitacion.CantidadCamas = cantidadCamas;
        habitacion.Precio = precio;

        Console.WriteLine("Habitación editada exitosamente.");
        Console.ReadLine();
    }

    static void RealizarReserva()
    {
        Console.Clear();

        
        bool habitacionesDisponibles = false;
        foreach (var h in habitaciones)
        {
            if (!h.Reservada)
            {
                habitacionesDisponibles = true;
                break;
            }
        }

        if (!habitacionesDisponibles)
        {
            Console.WriteLine("No hay habitaciones disponibles.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Habitaciones disponibles:");
        foreach (var h in habitaciones)
        {
            if (!h.Reservada)
                Console.WriteLine(h.ToString());
        }

        Console.Write("Ingrese el número de la habitación que desea reservar: ");
        string id = Console.ReadLine();

        Habitacion habitacion = habitaciones.Find(h => h.Id == id && !h.Reservada);


        if (habitacion == null)
        {
            Console.WriteLine("No se encontró una habitación disponible con ese ID.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese la cantidad de días para la reserva: ");
        int cantidadDias;
        while (!int.TryParse(Console.ReadLine(), out cantidadDias) || cantidadDias <= 0)
        {
            Console.WriteLine("Cantidad de días debe ser un número entero positivo. Intente de nuevo.");
            Console.Write("Ingrese la cantidad de días para la reserva: ");
        }

        Console.Write("Ingrese la cantidad de personas: ");
        int cantidadPersonas;
        while (!int.TryParse(Console.ReadLine(), out cantidadPersonas) || cantidadPersonas <= 0)
        {
            Console.WriteLine("Cantidad de personas debe ser un número entero positivo. Intente de nuevo.");
            Console.Write("Ingrese la cantidad de personas: ");
        }

        habitacion.Reservada = true;
        habitacion.DiasReservada = cantidadDias;
        habitacion.ValorTotal = habitacion.Precio * cantidadDias;

        Console.WriteLine($"Reserva realizada con éxito. Total a pagar: ${habitacion.ValorTotal}");
        Console.ReadLine();
    }
}
