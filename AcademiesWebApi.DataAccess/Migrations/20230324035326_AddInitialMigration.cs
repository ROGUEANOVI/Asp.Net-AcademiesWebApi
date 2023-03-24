using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademiesWebApi.DataAccess.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballTeam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    web = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: false),
                    email = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: false),
                    phone = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: false),
                    phone = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    schoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_schools",
                        column: x => x.schoolId,
                        principalTable: "School",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: false),
                    phone = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    schoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.id);
                    table.ForeignKey(
                        name: "FK_teachers_schools",
                        column: x => x.schoolId,
                        principalTable: "School",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", unicode: false, nullable: false),
                    teacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_teachers",
                        column: x => x.teacherId,
                        principalTable: "Teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    qualification = table.Column<double>(type: "float", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.id);
                    table.ForeignKey(
                        name: "FK_grades_courses",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_grades_students",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_courses_course",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_students_courses_student",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_teacherId",
                table: "Course",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_courseId",
                table: "Grade",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_studentId",
                table: "Grade",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_schoolId",
                table: "Student",
                column: "schoolId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_courseId",
                table: "StudentCourse",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_studentId",
                table: "StudentCourse",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_schoolId",
                table: "Teacher",
                column: "schoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballTeam");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "School");
        }
    }
}
