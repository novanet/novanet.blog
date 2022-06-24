
(function () {
    'use strict';
	
    angular
        .module('app')
	    .controller('controller', controller);
        
        controller.$inject = ['holidayService'];
        
        function controller(holidayService) {
            var vm = this;
            
            vm.decreaseYear = decreaseYear;
            vm.holidays = undefined;          
            vm.increaseYear = increaseYear;
            vm.months = [];            
            vm.selectedYear = undefined;
            vm.specialDays = [];
            vm.summerTime = undefined;            
            vm.today = moment().format();
			vm.totalNumberOfWorkingDaysInYear = undefined;
            vm.week = moment().isoWeek();
            
            function init() {
                vm.selectedYear = moment().year();                            
                setNumberOfWorkingDays();
                setSpecialDays();
            }
            
            function setNumberOfWorkingDays(){
                vm.holidays = holidayService.getHolidays(vm.selectedYear);

                vm.months = [];
                vm.months.push({'name' : 'Januar', 'numberOfWorkingDays': getNumberOfWorkingDays(1)});
                vm.months.push({'name' : 'Februar', 'numberOfWorkingDays': getNumberOfWorkingDays(2)});
                vm.months.push({'name' : 'Mars', 'numberOfWorkingDays': getNumberOfWorkingDays(3)});
                vm.months.push({'name' : 'April', 'numberOfWorkingDays': getNumberOfWorkingDays(4)});
                vm.months.push({'name' : 'Mai', 'numberOfWorkingDays': getNumberOfWorkingDays(5)});
                vm.months.push({'name' : 'Juni', 'numberOfWorkingDays': getNumberOfWorkingDays(6)});
                vm.months.push({'name' : 'Juli', 'numberOfWorkingDays': getNumberOfWorkingDays(7)});
                vm.months.push({'name' : 'August', 'numberOfWorkingDays': getNumberOfWorkingDays(8)});
                vm.months.push({'name' : 'September', 'numberOfWorkingDays': getNumberOfWorkingDays(9)});
                vm.months.push({'name' : 'Oktober', 'numberOfWorkingDays': getNumberOfWorkingDays(10)});
                vm.months.push({'name' : 'November', 'numberOfWorkingDays': getNumberOfWorkingDays(11)});
                vm.months.push({'name' : 'Desember', 'numberOfWorkingDays': getNumberOfWorkingDays(12)});      

				var totalDays = 0;
				for(var i = 0; i< vm.months.length; i++){
					totalDays += vm.months[i].numberOfWorkingDays;
				}
				vm.totalNumberOfWorkingDaysInYear = totalDays;			
            }
            
            function setSpecialDays(){
                vm.specialDays = holidayService.getSpecialDays(vm.selectedYear);
                vm.summerTime = {
                    start : holidayService.getStartOfSummerTime(vm.selectedYear),
                    end : holidayService.getEndOfSummerTime(vm.selectedYear)
                }                   
            }
            
            function getNumberOfWorkingDays(month){
                var lastDayOfMonth = moment(vm.selectedYear + '-' + month, 'YYYY-MM').daysInMonth();
                
                var numberOfWorkingDays = 0;
                for(var i=1; i<=lastDayOfMonth; i++){
                    var date = moment(new Date(vm.selectedYear, month-1, i));
                    
                    if(!isWeekend(date) && !isHoliday(date)){
                        numberOfWorkingDays++;
                    }
                }
                return numberOfWorkingDays;
            }
            
            function isHoliday(date){
                return _.some(vm.holidays, function(holiday){
                    return moment(holiday.date).isSame(date, 'day');                    
                });
            }
            
            function isWeekend(date){                
                return date.day() === 6 || date.day() === 0;  
            }
            
            function decreaseYear(){
                vm.selectedYear--;
                setNumberOfWorkingDays();
                setSpecialDays();
            }
            
            function increaseYear(){
                vm.selectedYear++;
                setNumberOfWorkingDays();
                setSpecialDays();
            }            
            
            init();      
        }
})();