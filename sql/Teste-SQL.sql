-- Teste SQL

-- Questao 1 

select count(p.idProcesso), st.dsStatus from tb_Processo p
inner join tb_Status st on st.idStatus = p.idStatus
group by st.dsStatus asc

-- Questao 2

select max(an.dtAndamento), p.nroProcesso from tb_Processo p
inner join tb_Andamento an on an.idProcesso = p.idProcesso
where YEAR(p.DtEncerramento) = 2013
group by p.nroProcesso

-- Questao 3

select count(DtEncerramento), DtEncerramento from tb_Processo
WHERE count(DtEncerramento) > 5
group by DtEncerramento asc

-- Questao 4

select REPLICATE('0', (10 - len(idProcesso)))