﻿// ------------------------------------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace ChangesService.Models
{
    /// <summary>
    /// Provides options for searching and filtering changelog data.
    /// </summary>
    public class ChangeLogSearchOptions : ChangeLogPagination
    {
        public string RequestUrl { get; }
        public string Workload { get; }
        public double DaysRange { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }

        public ChangeLogSearchOptions(string requestUrl = null, string workload = null, double daysRange = 0,
                                      DateTime? startDate = null, DateTime? endDate = null)
        {

            if (!string.IsNullOrEmpty(requestUrl) && !string.IsNullOrEmpty(workload))
            {
                throw new InvalidOperationException($"Cannot search by both {nameof(requestUrl)} and {nameof(workload)}.");
            }

            if ((startDate != null || endDate != null) && daysRange > 0)
            {
                throw new InvalidOperationException($"Cannot filter by both {nameof(startDate)} and {nameof(endDate)} " +
                    $"together with {nameof(daysRange)}.");
            }

            RequestUrl = requestUrl;
            Workload = workload;
            DaysRange = daysRange;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}