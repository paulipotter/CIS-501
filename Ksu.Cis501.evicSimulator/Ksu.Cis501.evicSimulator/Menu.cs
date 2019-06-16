using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis501.evicSimulator
{
    public class Menu
    {
        /// <summary>
        /// boolean field for switching between US and metric
        /// </summary>
        public static bool _isMetric = false;
        /// <summary>
        /// boolean field for checking if toggled has been activated
        /// </summary>
        public static bool _isToggled = false; //toggled means metric system on

        /// <summary>
        /// no parameter constructor
        /// </summary>
        public Menu()
        {

        }
        /// <summary>
        /// this method handles toggling the boolean fields _isMetric and _isToggled
        /// </summary>
        public virtual void toggle()
        {
            if (_isMetric == false)
            {
                _isMetric = true;
                _isToggled = true;
            }
            else if (_isMetric == true)
            {
                _isMetric = false;
                _isToggled = false;
            } 
        }
        /// <summary>
        /// This method takes care of displaying the appropriate settings and current menu
        /// </summary>
        public virtual void display()
        {
            Console.Clear();
            Console.WriteLine("Personal Settings");

            if (_isMetric == false)
            {
                Console.WriteLine("US Units");
            }
            else if (_isMetric == true)
            {
                Console.WriteLine("Metric Units");
            }

        }
        /// <summary>
        /// this method returns a string with the menu's name
        /// </summary>
        /// <returns></returns>
        public  override string ToString()
        {
            return "Personal Settings";
        }
        /// <summary>
        /// this method converts miles to kilometers
        /// </summary>
        /// <param name="mi"></param>
        /// <returns>returns the value in km calculated</returns>
        public static double ConvertToKM(double mi)
        {
            double km = mi * 1.609344;
            return km;
        }
        /// <summary>
        /// this method converts kilometers to miles
        /// </summary>
        /// <param name="km"></param>
        /// <returnsreturns>the value in miles calculated</returnsreturns>
        public static double ConvertToMI(double km)
        {
            double mi = km / 1.609344;
            return mi;

        }

    }//end class menu

    /// <summary>
    /// this is a class for the Status menu screen
    /// </summary>
    public class Status : Menu
    {
        /// <summary>
        /// this field to tell if we are incrementing the odometer
        /// </summary>
        bool _increment = false;
        /// <summary>
        /// field to hold current odometer reading 
        /// </summary>
        double _odometer = 0;
        /// <summary>
        /// field to hold the current oil change mileage
        /// </summary>
        double _oilChange = 0;
        /// <summary>
        /// field to tell if we are in the odometer setting or not 
        /// </summary>
        bool _isOdometer = false;

        /// <summary>
        /// getter for the oil change field
        /// </summary>
        public double Oil
        {
            get { return _oilChange; }
        }
        /// <summary>
        /// field taht toggles the _isOdometer boolean to change between settings
        /// </summary>
        public override void toggle()
        {

            if (_isOdometer == false)
            {
                _isOdometer = true;
            }
            else if (_isOdometer == true)
            {
                _isOdometer = false;
            }
          
        }
        /// <summary>
        /// this method displays the appropriate setting 
        /// </summary>
        /// <param name="randomOdometer">random number for starting odomerter</param>
        /// <param name="randomOilChange">random number for starting the oil change</param>
        public void display(int randomOdometer, int randomOilChange)
        {
            Console.Clear();
            Console.WriteLine("System Status");

           

            if (_increment == true)
            {
                _odometer = _odometer;
                _oilChange = _oilChange;
            }
            else
            { _odometer = randomOdometer;
            _oilChange = randomOilChange;

            }

            if (_isMetric == false && _isOdometer == true)
            {
                Console.WriteLine(_odometer.ToString() + " mi");
            }

            else if (_isMetric == true && _isOdometer == true)
            {
                _odometer = Menu.ConvertToKM(_odometer);

                Console.WriteLine(_odometer.ToString() + " km");

            }

            //settings for oil change
            if (_isMetric == false && _isOdometer == false)
            {
                Console.WriteLine("Next oil change in " + _oilChange.ToString() + " mi");
            }
            else if (_isMetric == true && _isOdometer == false)
            {
                _odometer = Menu.ConvertToKM(_odometer);
                Console.WriteLine("Next oil change in " + _oilChange.ToString()+ " km");
            }
            else if (_isMetric == false && _isOdometer == true && _isToggled == true)
            {
                _odometer = Menu.ConvertToMI(_odometer);
                Console.WriteLine(_odometer.ToString() + " mi");
            }

        }
        /// <summary>
        /// method to reset oilchange
        /// </summary>
        public void Reset()
        {
            Console.Clear();
            Console.WriteLine("System Status");

            if (_isMetric == true)
            {
                Console.WriteLine("Next oil change in 4828.032 km");
            }
            else
            {
                Console.WriteLine("Next oil change in 3000 mi");
            }
        }
        /// <summary>
        /// method to increment the value of odometer and decrement the value of oilchange, also takes care of the boolean
        /// </summary>
        public void Increment()
        {
            _odometer++;
            _oilChange--;
            _increment = true;
        }

    }
    /// <summary>
    /// this class represents the temperature menu screen
    /// </summary>
    public class Temperature : Menu
    {

        /// <summary>
        /// this field holds the inside temperature
        /// </summary>
        double _insideTemperature;
        /// <summary>
        /// this field holds the outside temperature
        /// </summary>
        double _outsideTemperature;
        /// <summary>
        /// this boolean represents which setting we are currently in 
        /// </summary>
        bool _isInside = false;
        /// <summary>
        /// this method toggles between the two settings 
        /// </summary>
        public override void toggle()
        {
           if (_isInside == false)
            {
                _isInside = true;
            }
           else if (_isInside == true)
            {
                _isInside = false;
            }
        }
        /// <summary>
        /// this method displays the current settings
        /// </summary>
        /// <param name="insideTemp">current inside temperature</param>
        /// <param name="outsideTemp">current outside temperature</param>
        public void display(int insideTemp, int outsideTemp)
        {
            Console.Clear();
            Console.WriteLine("Temperature Information");

            _outsideTemperature = outsideTemp;
            _insideTemperature = insideTemp;
            //inside variations
            if (_isInside == true && _isMetric == false)
            {
                Console.WriteLine(_insideTemperature + "°F Inside");
            }
            else if (_isInside == true && _isMetric == true)
            {
                _insideTemperature = ConvertToCelsius(_insideTemperature);
                    //(((_insideTemperature * 9) / 5) + 32);
                Console.WriteLine(_insideTemperature + "°C Inside");
            }
            //ouside variations
            else if (_isInside == false && _isMetric == false)
            {
                Console.WriteLine(_outsideTemperature + "°F Outside");
            }
            else if (_isInside == false && _isMetric == true)
            {
                
                _outsideTemperature = ConvertToCelsius(_outsideTemperature);
                //(((_outsideTemperature * 9) / 5) + 32);
                Console.WriteLine(_outsideTemperature + "°C Outside");
            }


        }

        /// <summary>
        /// converts parameter (farhernheit to celsius)
        /// </summary>
        /// <param name="f">farenheit number</param>
        /// <returns>new number in celsius units</returns>
        public double ConvertToCelsius(double f)
        {
            double c = (5.0 / 9.0) * (f-32);
            return c;
        }

        /// <summary>
        /// Converts parameter to farhenheit
        /// </summary>
        /// <param name="c">celsius number</param>
        /// <returns>resulting farhenheit number</returns>
        public double ConvertToFarhenheit(double c)
        {
            double f = ((c * 9)/5) + 32;
            return f;
        }

    } 
    /// <summary>
    /// this class implements the trip menu setting
    /// </summary>
    public class Trip : Menu
    {
        /// <summary>
        /// field for the increment boolean
        /// </summary>
        bool _increment = false;
        /// <summary>
        /// field to see which trip setting we are currently in
        /// </summary>
        bool _isTripA = false;
        /// <summary>
        /// this method is to toggle between the two trips
        /// </summary>
        public override void toggle()
        {
            if(_isTripA == false)
            {
                _isTripA = true;
            }
            else if (_isTripA == true)
            {
                _isTripA = false;
            }            
        }
        /// <summary>
        /// field with the distance in trip a
        /// </summary>
        double _tripA = 0;
        /// <summary>
        /// field with the ditance in trip b
        /// </summary>
        double _tripB = 0;
        /// <summary>
        /// this method displays the information in the current setting
        /// </summary>
        /// <param name="tripA">trip a distance</param>
        /// <param name="tripB">trip b distance</param>
        public void display(int tripA, int tripB)
        {
            Console.Clear();
            Console.WriteLine("Trip Information");


            if (_increment == true)
            {
                _tripA = _tripA;
                _tripB = _tripB;

            }
            else
            {
                _tripA = tripA;
                _tripB = tripB;
            }
            //initial print
            if (_isTripA == true && _isMetric == false )
            {
                Console.WriteLine("Trip-A: " + _tripA + " mi");
            }
            else if (_isTripA == false && _isMetric == true)
            {
                _tripB = Menu.ConvertToKM(_tripB);
                Console.WriteLine("Trip-B: " + _tripB + " km");
            }
            else if (_isTripA == true && _isMetric == true)
            {
                _tripA = Menu.ConvertToKM(_tripA);
                Console.WriteLine("Trip-A: " + _tripA + " km");
            }
            else if (_isTripA == false && _isMetric == false) //miles or kilometers atm????
            {
                Console.WriteLine("Trip-B: " + _tripB + " mi");
            }
            else if (_isTripA == true && _isMetric == false && _isToggled == false)
            {
                _tripA = Menu.ConvertToMI(_tripA);
                Console.WriteLine("Trip-A: " + _tripA + " mi");
            }
  

        } 
        /// <summary>
        /// resets the two trips
        /// </summary>
        public void Reset()
        {
            Console.Clear();
            Console.WriteLine("Trip Information");
            if (_isTripA == true)
            {
                _tripA = 0;

            }
            else
            {
                _tripB = 0;
            }
        }//do we want to display after resetting - maybe Paula.. Maybe.. 
        /// <summary>
        /// this method increments the trips
        /// </summary>
        public void Increment ()
        {
            _tripA++;
            _tripB++;

        }




    }
    /// <summary>
    /// this class implements the warning menu settings
    /// </summary>
    public class Warning : Menu
    {
        /// <summary>
        /// field for the door open or not
        /// </summary>
        bool _doorAjar = false;
        /// <summary>
        /// field for the engine alarm
        /// </summary>
        bool _CheckEngine = false;
        /// <summary>
        /// field for the oil change warning
        /// </summary>
        bool _oilChange = false;
            /// <summary> 
            /// this method takes care of toggling the warnings off and on
            /// </summary>
            /// <param name="a">door ajar toggle</param>
            /// <param name="b">check engine toggle</param>
            /// <param name="c">oil change toggle</param>
        public void toggle(bool a, bool b, bool c)
        { 
            //door settings
            if (_doorAjar == false  && a == true)
            {
                _doorAjar = true;
            }
            else if (_doorAjar == true && a == true)
            {
                _doorAjar = false;
            }
            //enginee settings
            else if (_CheckEngine == false && b == true)
            {
                _CheckEngine = true;
            }
            else if (_CheckEngine == true && b == true)
            {
                _CheckEngine = false;
            }
            // oil settings
            else if (_oilChange == false && c == true)
            {
                _oilChange = true;
            }
            else if (_oilChange == true && c == true)
            {
                _oilChange = false;
            } 
        }

        
        /// <summary>
        /// this method displays the information in the current setting
        /// </summary>
        public override void display()
        {
            Console.Clear();
            Console.WriteLine("Warning Messages");
           if (_doorAjar == true)
            {
                Console.WriteLine("Door Ajar!");
            }
           if (_CheckEngine == true)
            {
                Console.WriteLine("Check Engine Soon!");

            }
          if (_oilChange == true)
            {
                Console.WriteLine("Oil Change!");
            }

        }
       
       

    }
    


}
