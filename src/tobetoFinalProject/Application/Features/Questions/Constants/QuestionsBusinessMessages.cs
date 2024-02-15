namespace Application.Features.Questions.Constants;

public static class QuestionsBusinessMessages
{
    public const string QuestionNotExists = "Question not exists.";
    public const string QuestionOptionsLessThanSeven = "7'den fazla seçenek ekleyemezsiniz";
    public const string QuestionOptionsMustBeDifferent = "Ayný Seçeneði 1 den fazla kez ayný soruya ekleyemezsiniz";
    public const string PoolQuestionsMustBeDifferent = "Ayný Havuza Ayný Soruyu 2 veya daha fazla kere atamazsýnýz";
    public const string? QuestionOptionsHaveToContainCorrectOption = "Doðru seçenek sorunun tüm seçeneklerinin içerisinde yer almalý";
}