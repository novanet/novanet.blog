(function () {
	'use strict';

	angular
        .module('app')
        .factory('holidayService', holidayService);

	holidayService.$inject = [];

	function holidayService() {                                		
        return {
		    getHolidays : _.memoize(getHolidays),
                    getSpecialDays : _.memoize(getSpecialDays),
                    getStartOfSummerTime : _.memoize(getStartOfSummerTime),
                    getEndOfSummerTime : _.memoize(getEndOfSummerTime)
        };
        
        function getStartOfSummerTime(year){                                              
                // Sommertid start: kl. 0200 siste søndag i mars
                return getDateForLastOccurenceOfDayInMonth(year, 2, 0).format('YYYY-MM-DD');
        }
        
        function getEndOfSummerTime(year){                                              
                // Sommertid slutt: kl. 0300 siste søndag i oktober.
                return getDateForLastOccurenceOfDayInMonth(year, 9, 0).format('YYYY-MM-DD');
        }
        
        function getSpecialDays(year){
                var specialDays = [];
                
                // Morsdag: Den andre søndagen i februar
                specialDays.push({'date' : getMothersDay(year).format('YYYY-MM-DD'), 'name' : 'Morsdag'});
                
                // Farsdag: Den andre søndagen i november
                specialDays.push({'date' : getFathersDay(year).format('YYYY-MM-DD'), 'name' : 'Farsdag'});        
                
                return specialDays;                
        }	
                        
        function getHolidays(year) {
            var easterSunday = getEasterSunday(year);           
            
            var holidays = [];
            
            // 01.05: Offentlig høytidsdag        
            holidays.push({'date' : moment(new Date(year, 4, 1)).format('YYYY-MM-DD'), 'name' : 'Offentlig høytidsdag'});               
            
            // 17.05: Grunnlovsdag
            holidays.push({'date' : moment(new Date(year, 4, 17)).format('YYYY-MM-DD'), 'name' : 'Grunnlovsdag'});
            
	    // 24.12: Julaften
            holidays.push({'date' : moment(new Date(year, 11, 24)).format('YYYY-MM-DD'), 'name' : 'Julaften'});
		
            // 25.12: 1. juledag
            holidays.push({'date' : moment(new Date(year, 11, 25)).format('YYYY-MM-DD'), 'name' : '1. juledag'});
            
            // 26.12: 2. juledag
            holidays.push({'date' : moment(new Date(year, 11, 26)).format('YYYY-MM-DD'), 'name' : '2. juledag'});
		
	    // 31.12: Nyttårsaften
            holidays.push({'date' : moment(new Date(year, 11, 31)).format('YYYY-MM-DD'), 'name' : 'Nyttårsaften'});
            
            // 01.01: 1. nyttårsdag        
            holidays.push({'date' : moment(new Date(year, 0, 1)).format('YYYY-MM-DD'), 'name' : '1. nyttårsdag'});
            
            // XX.XX: Palmesøndag
            holidays.push({'date' : easterSunday.clone().add(-7, 'days').format('YYYY-MM-DD'), 'name' : 'Palmesøndag'});
            
            // XX.XX: Skjærtorsdag
            holidays.push({'date' : easterSunday.clone().add(-3, 'days').format('YYYY-MM-DD'), 'name' : 'Skjærtorsdag'});
            
            // XX.XX: Langfredag
            holidays.push({'date' : easterSunday.clone().add(-2, 'days').format('YYYY-MM-DD'), 'name' : 'Langfredag'});
            
            // XX.XX: 1. påskedag
            holidays.push({'date' : easterSunday.format('YYYY-MM-DD'), 'name' : '1. påskedag'});
            
            // XX.XX: 2. påskedag        
            holidays.push({'date' : easterSunday.clone().add(1, 'days').format('YYYY-MM-DD'), 'name' : '2. påskedag'});
            
            // XX.XX: Kristi Himmelfartsdag        
            holidays.push({'date' : easterSunday.clone().add(39, 'days').format('YYYY-MM-DD'), 'name' : 'Kristi Himmelfartsdag'});
            
            // XX.XX: 1. pinsedag
            holidays.push({'date' : easterSunday.clone().add(49, 'days').format('YYYY-MM-DD'), 'name' : '1. pinsedag'});
            
            // XX.XX: 2. pinsedag
            holidays.push({'date' : easterSunday.clone().add(50, 'days').format('YYYY-MM-DD'), 'name' : '2. pinsedag'});
            
            return holidays;
        }                  
        
        function getEasterSunday (year) {
                var a = year % 19;
                var b = Math.floor(year/100); 
                var c = year % 100;
                var d = Math.floor(b/4); 
                var e = b % 4;
                var f = Math.floor((b+8)/25);   
                var g = Math.floor((b-f+1)/3);
                var h = (19*a+b-d-g+15) % 30;      
                var i = Math.floor(c/4); 
                var k = c % 4;
                var l = (32 + 2*e + 2* i - h - k) % 7;
                var m = Math.floor((a+11*h+22*l)/451);
                var n = Math.floor((h+l-7*m+114)/31); 
                var p = (h+l-7*m+114) % 31;
                p++;
                return moment(new Date(year, n-1, p));                                
            }        
	}
        
        function getMothersDay(year){
                //den andre søndagen i februar
                return getDateForSecondOccurenceOfDayInMonth(year, 1, 0);                
        }
        
        function getFathersDay(year){
                // andre søndagen i november
                return getDateForSecondOccurenceOfDayInMonth(year, 10, 0);
        }
        
        function getDateForSecondOccurenceOfDayInMonth(year, month, day){
                var result = moment(new Date(year, month, 1)).add(7, 'days');               
                while (result.day() !== day) {
                        result.add(1, 'day');
                }
                return result;
        }
        
        function getDateForLastOccurenceOfDayInMonth(year, month, day){
                var result = moment(new Date(year, month, 1)).endOf('month');
                while (result.day() !== day) {
                        result.subtract(1, 'day');
                }
                return result;
        }
})();
