public interface Subject
{
    void RoundNotify();
    void TurnNotify();
}

public interface Observer
{
    void ObserverTurnUpdate(int round, int turn);
    void ObserverFinishUpdate(bool finish);
}