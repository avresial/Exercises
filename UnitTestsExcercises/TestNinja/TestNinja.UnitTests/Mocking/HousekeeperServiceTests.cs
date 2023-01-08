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
        private readonly string statementFilename = "filename";
        private Mock<IStatementSaver> statementSaver = new Mock<IStatementSaver>();
        private Mock<IEmailSender> emailService = new Mock<IEmailSender>();
        private Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        private HousekeeperService housekeeperService;
        private DateTime statementDate = new DateTime(2022, 1, 1);
        private Housekeeper housekeeper;

        public HousekeeperServiceTests()
        {
            housekeeper = new Housekeeper() { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            unitOfWork.Setup(x => x.Query<Housekeeper>())
                        .Returns(new List<Housekeeper> { housekeeper }
                        .AsQueryable);

            var xtraMessageBox = new Mock<IXtraMessageBox>();
            housekeeperService = new HousekeeperService(statementSaver.Object, emailService.Object, unitOfWork.Object, xtraMessageBox.Object);
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_ShouldGeneratesStatements()
        {
            var result = housekeeperService.SendStatementEmails(statementDate);

            statementSaver.Verify(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SendStatementEmails_WhenParameterIsInvalid_ShouldNotGenerateStatement(string email)
        {
            housekeeper.Email = email;

            var result = housekeeperService.SendStatementEmails(statementDate);

            statementSaver.Verify(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate), Times.Never);
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_EmailtheStatement()
        {
            statementSaver.Setup(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate))
                          .Returns(statementFilename);

            var result = housekeeperService.SendStatementEmails(statementDate);

            emailService.Verify(es => es.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, statementFilename, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SendStatementEmails_WhensatementFileIsInvalid_ShouldNotEmailTheStatement(string statement)
        {
            statementSaver.Setup(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate))
                          .Returns(statement);

            var result = housekeeperService.SendStatementEmails(statementDate);

            emailService.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
