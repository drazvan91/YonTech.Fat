using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;

namespace Yontech.Fat.Runner
{

  public class FatRunOptions
  {
    public List<Assembly> Assemblies { get; set; }
    public BrowserType Browser { get; set; }
    public bool ScreenShotOnFailure { get; set; }
    public string ReportFileLocation { get; set; }
    public int WaitAfterEachTestCase { get; set; }
    public int DelayBetweenInstructions { get; set; }
  }

}