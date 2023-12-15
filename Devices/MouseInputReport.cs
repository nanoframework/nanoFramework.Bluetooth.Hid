// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid.Devices
{
    /// <summary>
    /// A class representing the state of a <see cref="Mouse"/> device.
    /// </summary>
    public class MouseInputReport
    {
        /// <summary>
        /// Gets of sets the pressed <see cref="Mouse"/> button.
        /// </summary>
        public byte Button { get; private set; }

        /// <summary>
        /// Gets a value representing the target cursor position on the X-Axis.
        /// </summary>
        public sbyte X { get; private set; }

        /// <summary>
        /// Gets a value representing the target cursor position on the Y-Axis.
        /// </summary>
        public sbyte Y { get; private set; }

        /// <summary>
        /// Gets a value representing the target scroll position.
        /// </summary>
        public sbyte Wheel { get; private set; }

        /// <summary>
        /// Records a <see cref="Mouse"/> button press.
        /// </summary>
        /// <param name="button">The button to press.</param>
        public void Press(MouseButton button)
        {
            Button = (byte)button;
        }

        /// <summary>
        /// Releases the pressed mouse button.
        /// </summary>
        public void Release()
        {
            Button = 0x00;
        }

        /// <summary>
        /// Records a mouse movement towards a target position.
        /// </summary>
        /// <param name="x">The target X-Axis position..</param>
        /// <param name="y">The target Y-Axis position.</param>
        public void Move(sbyte x, sbyte y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Records a mouse wheen scroll action.
        /// </summary>
        /// <param name="wheelScroll">The amount to scroll the mouse wheel.</param>
        public void Scroll(sbyte wheelScroll)
        {
            Wheel = wheelScroll;
        }

        /// <summary>
        /// Resets the state of this instance.
        /// </summary>
        public void Reset()
        {
            Button = 0;
            X = 0;
            Y = 0;
            Wheel = 0;
        }

        /// <summary>
        /// Serializes this instance to a byte array as per the input report format.
        /// </summary>
        /// <returns>A byte array formatted as per the input report format.</returns>
        public byte[] ToBytes()
        {
            return new byte[4]
            {
                Button,
                (byte)X,
                (byte)Y,
                (byte)Wheel
            };
        }
    }
}
