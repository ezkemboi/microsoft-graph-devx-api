﻿// ------------------------------------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Linq;
using ChangesService.Models;
using Xunit;

namespace ChangesService.Test
{
    public class ChangeLogListModelShould
    {
        [Fact]
        public void UpdateTotalItemsOnChangeLogsPropertySetter()
        {
            // Arrange & Act
            var changeLogList = new ChangeLogList
            {
                ChangeLogs = GetChangeLogList().ChangeLogs
            };

            // 1st Assert
            Assert.Equal(3, changeLogList.TotalItems);
            Assert.Equal(3, changeLogList.ChangeLogs.Count());

            /* Take only first two changelog items from list,
             * e.g. in a pagination scenario.
             */
            changeLogList.ChangeLogs = changeLogList.ChangeLogs
                                       .Take(2)
                                       .ToList();

            // 2nd Assert - TotalItems value should not change
            Assert.Equal(3, changeLogList.TotalItems);
            Assert.Equal(2, changeLogList.ChangeLogs.Count());
        }

        [Fact]
        public void UpdateCurrentItemsOnChangeLogsPropertySetter()
        {
            // Arrange & Act
            var changeLogList = new ChangeLogList
            {
                ChangeLogs = GetChangeLogList().ChangeLogs
            };

            /* 1st Assert - CurrentItems should always be equal
             * to the current count of changelog items in list.
            */
            Assert.Equal(3, changeLogList.CurrentItems);
            Assert.Equal(changeLogList.CurrentItems, changeLogList.ChangeLogs.Count());

            /* Take only first two changelog items from list,
             * e.g. in a pagination scenario.
             */
            changeLogList.ChangeLogs = changeLogList.ChangeLogs
                                       .Take(2)
                                       .ToList();

            /* 2nd Assert - CurrentItems value should always change
             * with current count of changelog items in list.
            */
            Assert.Equal(2, changeLogList.CurrentItems);
            Assert.Equal(changeLogList.CurrentItems, changeLogList.ChangeLogs.Count());
        }

        [Fact]
        public void UpdateTotalPagesOnPageLimitPropertySetter()
        {
            // Arrange & Act
            var changeLogList = new ChangeLogList
            {
                ChangeLogs = GetChangeLogList().ChangeLogs
            };

            // Act
            changeLogList.PageLimit = 1;

            // Assert
            Assert.Equal(3, changeLogList.TotalPages);

            // Act
            changeLogList.PageLimit = 2;

            // Assert
            Assert.Equal(2, changeLogList.TotalPages);
        }

        /// <summary>
        /// Gets a sample of ChangeLog list
        /// </summary>
        /// <param name="variableDate">Optional. CreatedDateTime value for Reports
        /// workload.</param>
        /// <returns></returns>
        public static ChangeLogList GetChangeLogList(string variableDate = "2020-12-31T00:00:00.000Z")
        {
            // variableDate param will be used for specifying custom CreatedDateTime
            // value for Reports workload 

            var changeLogList = @"{
              ""changelog"": [
                 {
                   ""ChangeList"": [
                      {
                        ""Id"": ""6a6c7aa0-4b67-4d07-9ebf-c2bc1bcef553"",
                        ""ApiChange"": ""Resource"",
                        ""ChangedApiName"": ""ediscoveryCase,reviewSet,reviewSetQuery"",
                        ""ChangeType"": ""Addition"",
                        ""Description"": ""Introduced the compliance eDiscovery API, including the [ediscoveryCase](https://docs.microsoft.com/en-us/graph/api/resources/ediscoverycase?view=graph-rest-beta), [reviewSet](https://docs.microsoft.com/en-us/graph/api/resources/reviewset?view=graph-rest-beta), and [reviewSetQuery](https://docs.microsoft.com/en-us/graph/api/resources/reviewsetquery?view=graph-rest-beta), and operations."",
                        ""Target"": ""ediscoveryCase,reviewSet,reviewSetQuery""
                      }
                    ],
                   ""Id"": ""6a6c7aa0-4b67-4d07-9ebf-c2bc1bcef553"",
                   ""Cloud"": ""prd"",
                   ""Version"": ""beta"",
                   ""CreatedDateTime"": ""2020-06-01T00:00:00.000Z"",
                   ""WorkloadArea"": ""Compliance"",
                   ""SubArea"": ""eDiscovery""
                 },
                 {
                   ""ChangeList"": [
                      {
                        ""Id"": ""2d94636a-2d78-44d6-8b08-ff2a9121214b"",
                        ""ApiChange"": ""Resource"",
                        ""ChangedApiName"": ""schema extensions,Microsoft Cloud for US Government."",
                        ""ChangeType"": ""Addition"",
                        ""Description"": ""The [schema extensions](https://docs.microsoft.com/en-us/graph/api/resources/schemaextension) feature is now generally available in [Microsoft Cloud for US Government](https://docs.microsoft.com/en-us/graph/deployments)."",
                        ""Target"": ""schema extensions,Microsoft Cloud for US Government""
                      }
                    ],
                   ""Id"": ""2d94636a-2d78-44d6-8b08-ff2a9121214b"",
                   ""Cloud"": ""prd"",
                   ""Version"": ""v1.0"",
                   ""CreatedDateTime"": ""2020-09-15T00:00:00.000Z"",
                   ""WorkloadArea"": ""Extensions"",
                   ""SubArea"": ""Schema extensions""
                 },
                 {
                   ""ChangeList"": [
                      {
                        ""Id"": ""dca6467b-d026-4316-8353-2c6c02598af3"",
                        ""ApiChange"": ""Resource"",
                        ""ChangedApiName"": ""relyingPartyDetailedSummary,listing"",
                        ""ChangeType"": ""Addition"",
                        ""Description"": ""Added a new resource type [relyingPartyDetailedSummary](https://docs.microsoft.com/en-us/graph/api/resources/relyingpartydetailedsummary?view=graph-rest-beta). This resource type supports [listing](https://docs.microsoft.com/en-us/graph/api/relyingpartydetailedsummary-list?view=graph-rest-beta) the relying parties configured in Active Directory Federation Services."",
                        ""Target"": ""relyingPartyDetailedSummary,listing""
                      }
                    ],
                   ""Id"": ""dca6467b-d026-4316-8353-2c6c02598af3"",
                   ""Cloud"": ""prd"",
                   ""Version"": ""beta"",
                   ""CreatedDateTime"": ""variableDate"",
                   ""WorkloadArea"": ""Reports"",
                   ""SubArea"": ""Identity and access reports""
                 }
               ]
            }";

            changeLogList = changeLogList.Replace("variableDate", variableDate);

            return Services.ChangesService.DeserializeChangeLogList(changeLogList);
        }
    }
}
