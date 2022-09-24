namespace ParentalSight.Keyboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Service : IKeyboard, IParentalService
    {
        public void CaptureKeys(string filepath) => InternalCapture(filepath);

        public void Capture(string filepath) => InternalCapture(filepath);

        protected void InternalCapture(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}