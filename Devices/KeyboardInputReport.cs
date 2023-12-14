// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace nanoFramework.Bluetooth.HID.Devices
{
    /// <summary>
    /// A class representing a set of pressed keys on the keyboard.
    /// </summary>
    public class KeyboardInputReport
    {
        /// <summary>
        /// Gets the byte mask representing the pressed modifier keys.
        /// </summary>
        public byte Modifiers { get; private set; }

        /// <summary>
        /// Gets the OEM reserved byte.
        /// </summary>
        public byte ReservedByte { get; private set; }

        /// <summary>
        /// Gets the pressed keys.
        /// </summary>
        public byte[] Keys { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardInputReport"/> class.
        /// </summary>
        /// <param name="maxNumPressedKey">The maximum number of keys that can be pressed at one time.</param>
        public KeyboardInputReport(byte maxNumPressedKey)
        {
            Keys = new byte[maxNumPressedKey];
        }

        /// <summary>
        /// Add a key to the set of pressed keys.
        /// </summary>
        /// <param name="key">The pressed key.</param>
        /// <exception cref="InvalidOperationException">The maximum number of pressed keys has been reached.</exception>
        public void AddKey(byte key)
        {
            if (Devices.Keys.IsModifierKey(key))
            {
                var modifierKeyMask = 0x80 >> (7 - (key - Devices.Keys.Modifiers.LeftCtrl));
                Modifiers |= (byte)modifierKeyMask;

                return;
            }

            var firstAvailableSlot = -1;
            for (var slotIndex = 0; slotIndex < Keys.Length; slotIndex++)
            {
                var slotKey = Keys[slotIndex];

                // only 'press' the key if it has not been pressed before
                if (slotKey == key)
                {
                    return;
                }

                if (firstAvailableSlot == -1
                    && slotKey == 0x00)
                {
                    firstAvailableSlot = slotIndex;
                }
            }

            if (firstAvailableSlot > -1)
            {
                Keys[firstAvailableSlot] = key;
            }
            else
            {
                // throw if the key cannot go in the report
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Removes a key from the set of pressed keys.
        /// </summary>
        /// <param name="key">The key to "unpress".</param>
        public void RemoveKey(byte key)
        {
            for (var slotIndex = 0; slotIndex < Keys.Length; slotIndex++)
            {
                if (Keys[slotIndex] == key)
                {
                    Keys[slotIndex] = 0x00;
                }
            }
        }

        /// <summary>
        /// Resets and releases all keys and modifiers.
        /// </summary>
        public void Reset()
        {
            Modifiers = 0;
            ReservedByte = 0;

            for (var i = 0; i < Keys.Length; i++)
            {
                Keys[i] = 0x00;
            }
        }

        /// <summary>
        /// Serialize this instance to the input report byte array format.
        /// </summary>
        /// <returns>A byte array formatted as per the input report.</returns>
        public byte[] ToBytes()
        {
            var result = new byte[2 + Keys.Length];

            result[0] = Modifiers;
            result[1] = ReservedByte;

            Array.Copy(
                sourceArray: Keys,
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 2,
                length: Keys.Length);

            return result;
        }
    }
}
