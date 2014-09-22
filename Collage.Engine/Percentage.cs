namespace Collage.Engine
{
    using System;

    public class Percentage
    {
        private int value;

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.ValidateValue(value);

                this.value = value;
            }
        }

        public Percentage(int value)
        {
            this.ValidateValue(value);

            this.Value = value;
        }

        public float ValueAsFloat
        {
            get
            {
                return this.Value / 100f;
            }
        }

        private void ValidateValue(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", "Value must be greater or equal 0");
            }

            if (value > 100)
            {
                throw new ArgumentOutOfRangeException("value", "Value must be less or equal 100");
            }
        }
    }
}
