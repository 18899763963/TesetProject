<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <xsl:output method ="xml" version ="1.0" encoding ="gb2312" indent="yes" />
  <xsl:param name="baseFile"/>
  
  <xsl:template name="start" match="node()">
    <xsl:copy>
      <xsl:copy-of select="@*" />
      <xsl:apply-templates select="node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="Parameter">
    <xsl:copy>
      <xsl:copy-of select="@*" />
      <xsl:variable name ="CurrentType" select ="."/>
      <xsl:attribute name="base">
        <xsl:value-of select="$baseFile/xsd:schema/xsd:simpleType[@name=$CurrentType/@type]/xsd:restriction/@base"/>
      </xsl:attribute>
      <xsl:attribute name="length">
        <xsl:value-of select="$baseFile/xsd:schema/xsd:simpleType[@name=$CurrentType/@type]/xsd:restriction/xsd:length/@value"/>
      </xsl:attribute>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>

  <!--<xsl:variable name ="baseObj" select ="document('base.xsd')//xsd:simpleType"/>
  <xsl:variable name ="enumObj" select ="document('enum.xsd')//xsd:simpleType"/>-->

  <!--<xsl:param name="baseFile"/>
  <xsl:template match="/">
    <xsl:call-template name ="start"/>
  </xsl:template>-->

<!--<xsl:attribute name="baseFile">
        <xsl:value-of select="$baseFile/title"/>
      </xsl:attribute>-->
<!--<xsl:choose>
        <xsl:when test="$baseObj/@name=$CurrentType/@type">
          <xsl:attribute name="base">
            <xsl:value-of select="$baseObj[@name=$CurrentType/@type]/xsd:restriction/@base"/>
          </xsl:attribute>
          <xsl:attribute name="length">
            <xsl:value-of select="$baseObj[@name=$CurrentType/@type]/xsd:restriction/xsd:length/@value"/>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="$enumObj/@name=$CurrentType/@type">
          <xsl:attribute name="base">
            <xsl:value-of select="$enumObj[@name=$CurrentType/@type]/xsd:restriction/@base"/>
          </xsl:attribute>
          <xsl:attribute name="length">
            <xsl:value-of select="$enumObj[@name=$CurrentType/@type]/xsd:restriction/xsd:length/@value"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>-->
