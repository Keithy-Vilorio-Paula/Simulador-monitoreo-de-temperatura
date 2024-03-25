using System;
using System.Threading;

class Program 
{
    // Delegado para el evento de actualización de temperatura
    public delegate void TemperatureUpdateEventHandler(object sender, float temperature);

    // Evento de actualización de temperatura
    public static event TemperatureUpdateEventHandler TemperatureUpdated;

    static void Main(string[] args)
    {
        // Simulamos la llegada de datos de temperatura en un hilo separado
        Thread sensorThread = new Thread(SimulateTemperatureUpdates);
        sensorThread.Start();

        // Suscripción al evento de actualización de temperatura
        TemperatureUpdated += OnTemperatureUpdated;

        // Mantenemos la aplicación en funcionamiento para recibir eventos
        Console.WriteLine("La aplicación está esperando actualizaciones de temperatura. Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }

    static void SimulateTemperatureUpdates()
    {
        Random random = new Random();
        int updatesCount = 0;
        while (updatesCount < 10) // Simulamos 10 actualizaciones de temperatura
        {
            // Simulamos una nueva lectura de temperatura cada segundo
            float newTemperature = random.Next(0, 100);
            Console.WriteLine($"Nueva temperatura: {newTemperature}°C");

            // Disparamos el evento de actualización de temperatura
            TemperatureUpdated?.Invoke(null, newTemperature);

            updatesCount++;

            // Esperamos un segundo antes de la próxima actualización
            Thread.Sleep(1000);
        }
        Console.WriteLine("Simulación completa. No se enviarán más actualizaciones de temperatura.");
    }


    static void OnTemperatureUpdated(object sender, float temperature)
    {
        // Aquí puedes manejar la actualización de temperatura en tiempo real
        Console.WriteLine($"Temperatura actualizada: {temperature}°C");
    }
}
