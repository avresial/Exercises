using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class HousekeeperServiceTests
    {

        [Fact]
        public void SendStatementEmails_WhenCalled_ShouldGeneratesStatements()
        {
            var statementSaver = new Mock<IStatementSaver>();
            var emailService = new Mock<IEmailSender>();
            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(x => x.Query<Housekeeper>())
                .Returns(new List<Housekeeper> {
                    new Housekeeper() { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" }
                }.AsQueryable<Housekeeper>);

            var xtraMessageBox = new Mock<IXtraMessageBox>();
            var service = new HousekeeperService(statementSaver.Object, emailService.Object, unitOfWork.Object, xtraMessageBox.Object);

            var result = service.SendStatementEmails(new DateTime(2022, 1, 1));

            statementSaver.Verify(g => g.SaveStatement(1, "b", new DateTime(2022, 1, 1)));
        }
    }
}
