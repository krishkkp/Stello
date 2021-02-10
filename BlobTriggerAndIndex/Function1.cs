using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobTriggerAndIndex
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("hayablobcontainer/Documents//{name}", Connection = "DefaultEndpointsProtocol=https;AccountName=hayablob;AccountKey=cbGtHHHzbIFnsdaNhQglkLSoaLWS7GRmVIFPjBaNuItf3TFwKv4WfO1kp0lDF/a9ZvvvmZD64AHSj93XnZndOw==;EndpointSuffix=core.windows.net")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
