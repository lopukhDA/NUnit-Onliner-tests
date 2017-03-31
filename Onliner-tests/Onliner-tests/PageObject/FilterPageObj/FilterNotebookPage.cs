using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onliner_tests.PageObject.FilterPageObj
{
    class FilterNotebookPage : BasicFilterPage
    {
        private LoggerClass _log;
        private WebDriver _driver;

        public FilterNotebookPage(WebDriver driver, LoggerClass log) : base(driver, log)
        {
            _driver = driver;
            _log = log;
        }
        public By CpuIntelCoreI7 { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible .i-checkbox__real[value=intelcorei7]");
        public By CpuAMDa10 { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible .i-checkbox__real[value=amda10]");
        public By CpuAMDfx { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible .i-checkbox__real[value=amdfx]");
        public By CpuIntelAtom { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible .i-checkbox__real[value=intelatom]");
        public By CpuSamsung { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible .i-checkbox__real[value=samsung]");

        public By ShowOtherCPU { get; set; } = By.CssSelector("#schema-filter > div:nth-child(1) > div:nth-child(11) > div.schema-filter__facet > div.schema-filter-control.schema-filter-control_more");
        public By FilterPopoverVisible { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible");

        public enum CpuType
        {
            IntelCoreI7, AMDa10, AMDfx, IntelAtom, Samsung
        }

        public void SelectCPU(CpuType orderType)
        {
            _driver.Click(ShowOtherCPU);
            _driver.WaitElement(FilterPopoverVisible);
            switch (orderType)
            {
                case CpuType.IntelCoreI7:
                    _driver.Click(CpuIntelCoreI7);
                    break;
                case CpuType.AMDa10:
                    _driver.Click(CpuAMDa10);
                    break;
                case CpuType.AMDfx:
                    _driver.Click(CpuAMDfx);
                    break;
                case CpuType.IntelAtom:
                    _driver.Click(CpuIntelAtom);
                    break;
                case CpuType.Samsung:
                    _driver.Click(CpuSamsung);
                    break;
            }
        }

    }
}
