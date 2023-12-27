using System;

public class VehiclesFactory
{
	public VehiclesFactory()
	{
	}
	public static IVehicles getVahicles(int type)
	{
		IVehicles ObjectType = null;
		if(type == 1)
		{
			ObjectType = new Car();

		}
		else if (type == 2)
        {
            ObjectType = new Truck();

        }
        else if (type == 3)
        {
            ObjectType = new Bike();

        }
        else if (type == 4)
        {
            ObjectType = new Aeroplane();

        }
        else if (type == 5)
        {
            ObjectType = new Train();

        }
        else if (type == 6)
        {
            ObjectType = new Bus();

        }
        return ObjectType;
	}
}


