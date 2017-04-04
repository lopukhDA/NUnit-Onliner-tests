using OpenQA.Selenium;

namespace Onliner_tests.PageObject.FilterPageObj
{
    class FilterNotebookComponent : BasicFilterComponent
    {
        private WebDriver _driver;

        public FilterNotebookComponent(WebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public By CpuIntelCoreI7 { get; set; } = By.CssSelector(".schema-filter-popover_visible input[value=intelcorei7]+span");
        public By CpuAMDa10 { get; set; } = By.CssSelector(".schema-filter-popover_visible input[value=amda10]+span");
        public By CpuAMDfx { get; set; } = By.CssSelector(".schema-filter-popover_visible input[value=amdfx]+span");
        public By CpuIntelAtom { get; set; } = By.CssSelector(".schema-filter-popover_visible input[value=intelatom]+span");
        public By CpuSamsung { get; set; } = By.CssSelector(".schema-filter-popover_visible input[value=samsung]+span");

        public By ShowOtherCPU { get; set; } = By.XPath("//span[contains(text(),'Процессор')]//..//..//div[contains(@data-bind, 'facet.togglePopover')]");
        public By FilterPopoverVisible { get; set; } = By.CssSelector(".schema-filter-popover.schema-filter-popover_visible");

        public enum CpuType
        {
            IntelCoreI7, AMDa10, AMDfx, IntelAtom, Samsung
        }

        public void SelectCPU(CpuType orderType)
        {
            _driver.WaitForElementIsVisible(Filter);
            var proc = _driver.GetElement(ShowOtherCPU);
            _driver.Scroll((proc.Location.Y).ToString());
            _driver.Click(ShowOtherCPU);
            _driver.WaitForElementIsVisible(FilterPopoverVisible);
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
            WaitComplitePrice();
        }

    }
}
