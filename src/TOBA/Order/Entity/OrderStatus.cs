namespace TOBA.Order.Entity
{
	public enum OrderStatus
	{
		/// <summary>
		/// δ֪
		/// </summary>
		Unknown,
		/// <summary>
		/// δ֧��
		/// </summary>
		NotPay,
		/// <summary>
		/// ��֧��
		/// </summary>
		Paid,
		/// <summary>
		/// �ѳ�Ʊ
		/// </summary>
		TicketPrinted,
		/// <summary>
		/// �ѳ�վ
		/// </summary>
		Used,
		/// <summary>
		/// ����Ʊ
		/// </summary>
		Refunded,
		/// <summary>
		/// �Ѹ�ǩ
		/// </summary>
		Resigned,
		/// <summary>
		/// ��ǩƱ
		/// </summary>
		ResignTicket,
		/// <summary>
		/// �Ѹ�ǩ��֧��
		/// </summary>
		ResignNotPaid,
		/// <summary>
		/// �Ŷ���
		/// </summary>
		Queue,
		/// <summary>
		/// ���ڸ�ǩ
		/// </summary>
		BeResigned,
		/// <summary>
		/// ��Ʊʧ��
		/// </summary>
		Failed,
		/// <summary>
		/// �ѽ�վ
		/// </summary>
		Leaved,
		/// <summary>
		/// �����վ��֧��
		/// </summary>
		ResignChangeTsNotPaid,
		/// <summary>
		/// �����վ��
		/// </summary>
		ResignChangeTsIng,
		/// <summary>
		/// �����վƱ
		/// </summary>
		ResignChagneTsTicket,
		/// <summary>
		/// �ѱ����վƱ
		/// </summary>
		ResignChagneTsed,
		/// <summary>
		/// ����֧��
		/// </summary>
		CreditPaid
	}
}

