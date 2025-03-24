namespace APSW_L_1
{
   public class Hardware
    {
        private int _CPU;
        private int _RAM;
        private int _GPU;
        private int _HDD;

        public List<EController> controllers { get; set; }
        public int CPU_quality
        {
            get => this._CPU;
            private set
            {
                if (value > 0 && value <= 5)
                {
                    this._CPU = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }

        public int RAM_capacity
        {
            get => this._RAM;
            private set
            {
                if (value > 0 && value <= 32)
                {
                    this._RAM = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }

        public int GPU_quality
        {
            get => this._GPU;
            private set
            {
                if (value > 0 && value <= 5)
                {
                    this._GPU = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }

        public int HDD_capacity
        {
            get => this._HDD;
            private set
            {
                if (value >= 0 && value <= 100)
                {
                    this._HDD = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), _HDD,"You cannot download this game. Your HDD is full.");
                }
            }
        }

        public static List<EController> ControllersForPC()
        {
            return new List<EController>() { EController.Keyboard, EController.Mouse };
        }
        public static List<EController> ControllersForConsole()
        {
            return new List<EController>() { EController.Gamepad };
        }
        public static List<EController> ControllersForMobile()
        {
            return new List<EController>() {};
        }
        public Hardware(int cpu, int ram, int gpu, int hdd, List<EController> controllers)
        {
            CPU_quality = cpu;
            RAM_capacity = ram;
            GPU_quality = gpu;
            HDD_capacity = hdd;
            this.controllers = controllers;
        }

        public void FillCapasity(int capasity)
        { 
            HDD_capacity -= capasity;            
        }
    }
}
