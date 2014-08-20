using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.Company
{
    public static class Grammar
    {
        public static ParserFn<Company> Top()
        {
            ParserFn<string> companyName = Parser.RegEx(@"\w+", "company name");
            ParserFn<Employee> employee = new[] {Parser.RegEx(@"\w+", "first name"), Parser.String(" "), Parser.RegEx(@"\w+", "last name")}
                .And()
                .Select(values => values.ToList())
                .Select(values => new Employee() {Name = new PersonName() {FirstName = values[0], LastName = values[2]}})
                .And(Parser.Char(',').Optional(), (a, b) => a)
                .And(Parser.RegEx(@"\s*"), (a, b) => a, "employee");
            ParserFn<Company> company = new[] { companyName, Parser.String(" { ") }.And()
                .And(employee.AtLeastOnce(), (values, employees) => new Company { CompanyName = values.ToList()[0], Employees = employees })
                .And(Parser.Char('}'), (a, b) => a, "company");
            return company;
        }

        public static string Test()
        {
            ParserFn<Company> rule = Top();
            var state = new State("Microsoft { Ivan Ivanov, Peter Petrov, Sidor Sidorov }");
            ParseResult<Company> result = state.Apply(rule);
            return state.debugInfo.ToString();
        }
    }
}
