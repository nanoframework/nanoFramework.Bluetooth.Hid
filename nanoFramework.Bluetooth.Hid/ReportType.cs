// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid
{
    /// <summary>
    /// The report type to use when creating data reports.
    /// </summary>
    public enum ReportType : byte
    {
        /// <summary>
        /// Input report.
        /// </summary>
        Input = 0x01,

        /// <summary>
        /// Output report.
        /// </summary>
        Output = 0x02,

        /// <summary>
        /// Feature report.
        /// </summary>
        Feature = 0x03
    }
}
